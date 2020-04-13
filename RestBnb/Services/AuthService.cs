using RestBnb.API.Helpers;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Constants;
using RestBnb.Core.Entities;
using RestBnb.Core.Services;
using RestBnb.Infrastructure.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RestBnb.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsersService _userService;
        private readonly IEmailSender _emailSender;
        private readonly IAuthenticationServiceHelper _authenticationServiceHelper;
        private readonly IRefreshTokensService _refreshTokensService;
        private readonly IStringHasherService _stringHasherService;

        public AuthService(
            IUsersService userService,
            IEmailSender emailSender,
            IAuthenticationServiceHelper authenticationServiceHelper,
            IRefreshTokensService refreshTokensService,
            IStringHasherService stringHasherService)
        {
            _userService = userService;
            _authenticationServiceHelper = authenticationServiceHelper;
            _refreshTokensService = refreshTokensService;
            _stringHasherService = stringHasherService;
            _emailSender = emailSender;
        }

        /// <summary>
        /// Registers new user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        public async Task<AuthenticationResult> RegisterAsync(string email, string password)
        {
            var existingUser = await _userService.GetUserByEmailAsync(email);

            if (existingUser != null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User with this email address already exists" }
                };
            }

            var (hash, salt) = _stringHasherService.HashStringWithHmacAndSalt(password);

            var newUser = new User
            {
                Email = email,
                PasswordHash = hash,
                PasswordSalt = salt
            };

            var created = await _userService.CreateUserAsync(newUser);

            if (!created)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "Could not create user." }
                };
            }

            var assignedToRole = await _userService.AddToRoleAsync(newUser, ApiRoles.User);

            if (!assignedToRole)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "Could not assign user to default role." }
                };
            }

            var emailSent = await _emailSender.SendEmailAsync(newUser.Email, "Welcome to RestBnb!", "Thank you for signing up.");

            if (emailSent.StatusCode != HttpStatusCode.Accepted)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "Could not send confirmation email." }
                };
            }

            return await _authenticationServiceHelper.GetAuthenticationResultAsync(newUser);
        }

        /// <summary>
        /// Can be used to login user and return validation token
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            var user = await _userService.GetUserByEmailAsync(email);

            if (user == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User does not exist" }
                };
            }

            var userHasValidPassword = await _userService.VerifyPasswordAsync(email, password);

            if (!userHasValidPassword)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "Username or password is incorrect" }
                };
            }

            return await _authenticationServiceHelper.GetAuthenticationResultAsync(user);
        }

        /// <summary>
        /// Refreshes JWT validation token when the previous one expires
        /// </summary>
        /// <param name="token"></param>
        /// <param name="refreshToken"></param>
        public async Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken)
        {
            var validatedToken = _authenticationServiceHelper.GetPrincipalFromToken(token);

            if (validatedToken == null)
            {
                return new AuthenticationResult { Errors = new[] { "This token is invalid" } };
            }

            var expiryDateUnix = long.Parse(validatedToken
                .Claims
                .Single(x => x.Type == JwtRegisteredClaimNames.Exp)
                .Value);

            var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(expiryDateUnix);

            if (expiryDateTimeUtc > DateTime.UtcNow)
            {
                return new AuthenticationResult { Errors = new[] { "This token hasn't expired yet" } };
            }

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            var storedRefreshToken = await _refreshTokensService.GetRefreshTokenByTokenAsync(refreshToken);
            if (storedRefreshToken == null
                || DateTime.UtcNow > storedRefreshToken.ExpiryDate
                || storedRefreshToken.Invalidated
                || storedRefreshToken.Used
                || storedRefreshToken.JwtId != jti)
            {
                return new AuthenticationResult { Errors = new[] { "This refresh token is not valid" } };
            }

            storedRefreshToken.Used = true;
            await _refreshTokensService.UpdateRefreshTokenAsync(storedRefreshToken);

            var user = await _userService.GetUserByIdAsync(int.Parse(validatedToken.Claims.Single(x => x.Type == "id").Value));

            var response = await _authenticationServiceHelper.GetAuthenticationResultAsync(user);
            return response;
        }
    }
}