using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Contracts.V1;
using RestBnb.Core.Entities;
using RestBnb.Core.Helpers;
using RestBnb.Core.Options;
using RestBnb.Infrastructure;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RestBnb.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsersService _userService;
        private readonly JwtSettings _jwtSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly DataContext _dataContext;

        public AuthService(
            IUsersService userService,
            DataContext context,
            TokenValidationParameters tokenValidationParameters,
            JwtSettings jwtSettings)
        {
            _userService = userService;
            _dataContext = context;
            _tokenValidationParameters = tokenValidationParameters;
            _jwtSettings = jwtSettings;
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

            var (hash, salt) = StringHasherHelper.HashStringWithHmacAndSalt(password);

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

            return await GetAuthenticationResultAsync(newUser);
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

            var userHasValidPassword = await _userService.CheckPasswordAsync(email, password);

            if (!userHasValidPassword)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "Username or password is incorrect" }
                };
            }

            return await GetAuthenticationResultAsync(user);
        }

        /// <summary>
        /// Refreshes JWT validation token when the previous one expires
        /// </summary>
        /// <param name="token"></param>
        /// <param name="refreshToken"></param>
        public async Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken)
        {
            var validatedToken = GetPrincipalFromToken(token);

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

            var storedRefreshToken = await _dataContext.RefreshTokens.SingleOrDefaultAsync(x => x.Token == refreshToken);

            if (storedRefreshToken == null
                || DateTime.UtcNow > storedRefreshToken.ExpiryDate
                || storedRefreshToken.Invalidated
                || storedRefreshToken.Used
                || storedRefreshToken.JwtId != jti)
            {
                return new AuthenticationResult { Errors = new[] { "This refresh token is not valid" } };
            }

            storedRefreshToken.Used = true;
            _dataContext.RefreshTokens.Update(storedRefreshToken);
            await _dataContext.SaveChangesAsync();

            var user = await _userService.GetUserByIdAsync(int.Parse(validatedToken.Claims.Single(x => x.Type == "id").Value));

            return await GetAuthenticationResultAsync(user);
        }

        private async Task<AuthenticationResult> GetAuthenticationResultAsync(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("id", user.Id.ToString())
            };

            var userRoles = await _userService.GetRolesAsync(user);
            if (userRoles != null)
            {
                claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role.Name)));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifetime),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                UserId = user.Id,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6)
            };

            await _dataContext.RefreshTokens.AddAsync(refreshToken);
            await _dataContext.SaveChangesAsync();

            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Token
            };
        }

        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var tokenValidationParameters = _tokenValidationParameters.Clone();
                tokenValidationParameters.ValidateLifetime = false;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

                return !IsJwtWithValidSecurityAlgorithm(validatedToken) ? null : principal;
            }
            catch
            {
                return null;
            }
        }

        private static bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return validatedToken is JwtSecurityToken jwtSecurityToken &&
                   jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                       StringComparison.InvariantCultureIgnoreCase);
        }
    }
}