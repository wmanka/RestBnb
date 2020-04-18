using RestBnb.API.Helpers;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Constants;
using RestBnb.Core.Entities;
using RestBnb.Core.Services;
using RestBnb.Infrastructure.Services;
using System.Linq;
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

        public async Task<AuthResponse> RegisterAsync(string email, string password)
        {
            var (hash, salt) = _stringHasherService.HashStringWithHmacAndSalt(password);

            var user = new User
            {
                Email = email,
                PasswordHash = hash,
                PasswordSalt = salt
            };

            await _userService.CreateUserAsync(user);
            await _userService.AddToRoleAsync(user, ApiRoles.User);
            await _emailSender.SendEmailAsync(user.Email, "Welcome to RestBnb!", "Thank you for signing up.");

            return await _authenticationServiceHelper.GetAuthenticationResultAsync(user);
        }

        public async Task<AuthResponse> LoginAsync(string email, string password)
        {
            var user = await _userService.GetUserByEmailAsync(email);

            return await _authenticationServiceHelper.GetAuthenticationResultAsync(user);
        }

        public async Task<AuthResponse> RefreshTokenAsync(string token, string refreshToken)
        {
            var validatedToken = _authenticationServiceHelper.GetPrincipalFromToken(token);
            var storedRefreshToken = await _refreshTokensService.GetRefreshTokenByTokenAsync(refreshToken);

            storedRefreshToken.Used = true;

            await _refreshTokensService.UpdateRefreshTokenAsync(storedRefreshToken);

            var user = await _userService.GetUserByIdAsync(int.Parse(validatedToken.Claims.Single(x => x.Type == "id").Value));

            var response = await _authenticationServiceHelper.GetAuthenticationResultAsync(user);

            return response;
        }
    }
}