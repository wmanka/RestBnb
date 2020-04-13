using Microsoft.AspNetCore.Mvc;
using Moq;
using RestBnb.API.Controllers.V1;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Contracts.V1.Requests.Auth;
using RestBnb.Core.Contracts.V1.Responses;
using RestBnb.Core.Entities;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RestBnb.UnitTests.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _authServiceMock;

        public AuthControllerTests()
        {
            _authServiceMock = new Mock<IAuthService>();
        }

        [Fact]
        public async Task GivenCorrectEmailAndPasswordOfNonExistingUser_WhenRegistering_ThenReturnsAuthSuccessResponse()
        {
            const string email = "email@test.com";
            const string password = "Password1!";
            var userRegistrationRequest = new UserRegistrationRequest
            {
                Email = email,
                Password = password
            };
            const string token = "token";
            const string refreshToken = "refresh token";
            _authServiceMock.Setup(repo => repo.RegisterAsync(email, password))
                .ReturnsAsync(new AuthenticationResult { Success = true, Token = token, RefreshToken = refreshToken });
            var controller = new AuthController(_authServiceMock.Object);

            var result = await controller.Register(userRegistrationRequest);

            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var authSuccessResponse = Assert.IsType<AuthSuccessResponse>(okObjectResult.Value);
            Assert.Equal(token, authSuccessResponse.Token);
            Assert.Equal(refreshToken, authSuccessResponse.RefreshToken);
        }

        [Fact]
        public async Task GivenCorrectEmailAndPasswordOfAlreadyExistingUser_WhenRegistering_ThenReturnsAuthSuccessResponse()
        {
            const string email = "email@test.com";
            const string password = "Password1!";
            var userRegistrationRequest = new UserRegistrationRequest
            {
                Email = email,
                Password = password
            };
            const string errorMessage = "User with this email address already exists";
            _authServiceMock.Setup(repo => repo.RegisterAsync(email, password))
                .ReturnsAsync(new AuthenticationResult { Success = false, Errors = new[] { errorMessage } });
            var controller = new AuthController(_authServiceMock.Object);

            var result = await controller.Register(userRegistrationRequest);

            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            var authFailedResponse = Assert.IsType<AuthFailedResponse>(badRequestObjectResult.Value);
            Assert.NotEmpty(authFailedResponse.Errors);
            Assert.Equal(errorMessage, authFailedResponse.Errors.First());
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("wrongEmail", "Password1!")]
        [InlineData("test@email.com", "wrongPassword")]
        public async Task GivenIncorrectEmailAndPassword_WhenRegistering_ThenReturnsBadRequestResponse(string email, string password)
        {
            var userRegistrationRequest = new UserRegistrationRequest
            {
                Email = email,
                Password = password
            };
            const string errorMessage = "Could not create user.";
            _authServiceMock.Setup(repo => repo.RegisterAsync(email, password))
                .ReturnsAsync(new AuthenticationResult { Success = false, Errors = new[] { errorMessage } });
            var controller = new AuthController(_authServiceMock.Object);

            var result = await controller.Register(userRegistrationRequest);

            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            var authFailedResponse = Assert.IsType<AuthFailedResponse>(badRequestObjectResult.Value);
            Assert.Equal(errorMessage, authFailedResponse.Errors.First());
        }

        [Fact]
        public async Task GivenCorrectEmailAndPasswordOfExistingUser_WhenLoggingIn_ThenReturnsAuthSuccessResponse()
        {
            const string email = "email@test.com";
            const string password = "Password1!";
            var userLoginRequest = new UserLoginRequest
            {
                Email = email,
                Password = password
            };
            _authServiceMock.Setup(repo => repo.LoginAsync(email, password))
                .ReturnsAsync(new AuthenticationResult { Success = true, Token = "token", RefreshToken = "refresh token" });
            var controller = new AuthController(_authServiceMock.Object);

            var result = await controller.Login(userLoginRequest);

            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var authSuccessResponse = Assert.IsType<AuthSuccessResponse>(okObjectResult.Value);
            Assert.Equal("token", authSuccessResponse.Token);
            Assert.Equal("refresh token", authSuccessResponse.RefreshToken);
        }

        [Fact]
        public async Task GivenCorrectEmailAndPasswordOfNonExistingUser_WhenLoggingIn_ThenReturnsAuthFailedResponse()
        {
            const string email = "email@test.com";
            const string password = "Password1!";
            var userLoginRequest = new UserLoginRequest
            {
                Email = email,
                Password = password
            };
            const string errorMessage = "User with this email address already exists";
            _authServiceMock.Setup(repo => repo.LoginAsync(email, password))
                .ReturnsAsync(new AuthenticationResult { Success = false, Errors = new[] { errorMessage } });
            var controller = new AuthController(_authServiceMock.Object);

            var result = await controller.Login(userLoginRequest);

            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            var authFailedResponse = Assert.IsType<AuthFailedResponse>(badRequestObjectResult.Value);
            Assert.Equal(errorMessage, authFailedResponse.Errors.First());
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("wrongEmail", "Password1!")]
        [InlineData("test@email.com", "wrongPassword")]
        public async Task GivenIncorrectEmailAndPassword_WhenLoggingIn_ThenReturnsBadRequestResponse(string email, string password)
        {
            var userLoginRequest = new UserLoginRequest
            {
                Email = email,
                Password = password
            };
            const string errorMessage = "Username or password is incorrect";
            _authServiceMock.Setup(repo => repo.LoginAsync(email, password))
                .ReturnsAsync(new AuthenticationResult { Success = false, Errors = new[] { errorMessage } });
            var controller = new AuthController(_authServiceMock.Object);

            var result = await controller.Login(userLoginRequest);

            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            var authFailedResponse = Assert.IsType<AuthFailedResponse>(badRequestObjectResult.Value);
            Assert.Equal(errorMessage, authFailedResponse.Errors.First());
        }
    }
}
