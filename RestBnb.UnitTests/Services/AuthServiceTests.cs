using Moq;
using RestBnb.API.Helpers;
using RestBnb.API.Services;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Entities;
using RestBnb.Core.Services;
using RestBnb.Infrastructure.Services;
using SendGrid;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RestBnb.UnitTests.Services
{
    public class AuthServiceTests
    {
        private readonly Mock<IUsersService> _userServiceMock;
        private readonly Mock<IEmailSender> _emailSenderMock;
        private readonly Mock<IAuthenticationServiceHelper> _authenticationResultHelperMock;
        private readonly Mock<IRefreshTokensService> _refreshTokensService;
        private readonly Mock<IStringHasherService> _stringHasherServiceMock;

        public AuthServiceTests()
        {
            _stringHasherServiceMock = new Mock<IStringHasherService>();
            _userServiceMock = new Mock<IUsersService>();
            _emailSenderMock = new Mock<IEmailSender>();
            _authenticationResultHelperMock = new Mock<IAuthenticationServiceHelper>();
            _refreshTokensService = new Mock<IRefreshTokensService>();
        }

        [Fact]
        public async Task GivenNonExistingEmailAndPassword_WhenRegistering_ThenReturnsAuthenticationResultWithTokens()
        {
            const string email = "test@email.com";
            const string password = "Password1!";
            const string token = "token";
            const string refreshToken = "refresh token";
            var hash = Encoding.ASCII.GetBytes(password);
            var salt = Encoding.ASCII.GetBytes("salt");
            var authenticationResult = new AuthenticationResult
            {
                Success = true,
                Token = token,
                RefreshToken = refreshToken
            };
            _userServiceMock.Setup(usersService =>
                 usersService.GetUserByEmailAsync(email)).ReturnsAsync(null as User);
            _stringHasherServiceMock.Setup(stringHasherService =>
                stringHasherService.HashStringWithHmacAndSalt(password)).Returns((hash, salt));
            _userServiceMock.Setup(usersService =>
                usersService.CreateUserAsync(It.Is<User>(x => x.Email == email))).ReturnsAsync(true);
            _userServiceMock.Setup(usersService =>
                usersService.AddToRoleAsync(It.Is<User>(x => x.Email == email), It.IsAny<string>())).ReturnsAsync(true);
            _emailSenderMock.Setup(emailSender =>
                emailSender.SendEmailAsync(email, It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new Response(HttpStatusCode.Accepted, null, null));
            _authenticationResultHelperMock.Setup(authenticationServiceHelper =>
                authenticationServiceHelper.GetAuthenticationResultAsync(It.Is<User>(x => x.Email == email)))
                .ReturnsAsync(authenticationResult);
            var authService = new AuthService(
                _userServiceMock.Object,
                _emailSenderMock.Object,
                _authenticationResultHelperMock.Object,
                _refreshTokensService.Object,
                _stringHasherServiceMock.Object);

            var result = await authService.RegisterAsync(email, password);

            Assert.IsAssignableFrom<AuthenticationResult>(result);
            Assert.True(result.Success);
            Assert.Null(result.Errors);
        }
    }
}
