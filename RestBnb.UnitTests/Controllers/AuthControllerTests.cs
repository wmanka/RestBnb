using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RestBnb.API.Application.Auth.Commands;
using RestBnb.API.Application.Auth.Requests;
using RestBnb.API.Application.Auth.Responses;
using RestBnb.API.Controllers.V1;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RestBnb.UnitTests.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IServiceProvider> _serviceProviderMock;

        public AuthControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _mapperMock = new Mock<IMapper>();
            _serviceProviderMock = new Mock<IServiceProvider>();
        }

        [Fact]
        public async Task GivenValidEmailAndPasswordOfNonExistingUser_WhenRegistering_ThenReturnsAuthSuccessResponse()
        {
            const string email = "email@test.com";
            const string password = "Password1!";
            var request = new UserRegistrationRequest
            {
                Email = email,
                Password = password
            };
            const string token = "token";
            const string refreshToken = "refresh token";
            _mediatorMock.Setup(mediator =>
                mediator.Send(It.Is<UserRegistrationCommand>(x =>
                    x.Email == email && x.Password == password), CancellationToken.None))
                .ReturnsAsync(new AuthResponse { Token = token, RefreshToken = refreshToken });
            var controller = new AuthController(_mapperMock.Object, _mediatorMock.Object);

            var result = await controller.Register(request);

            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var authSuccessResponse = Assert.IsType<AuthResponse>(okObjectResult.Value);
            Assert.Equal(token, authSuccessResponse.Token);
            Assert.Equal(refreshToken, authSuccessResponse.RefreshToken);
        }

        [Fact]
        public async Task GivenEmailAndPasswordOfExistingUser_WhenLoggingIn_ThenReturnsAuthSuccessResponse()
        {
            const string email = "email@test.com";
            const string password = "Password1!";
            var userLoginRequest = new UserLoginRequest
            {
                Email = email,
                Password = password
            };
            const string token = "token";
            const string refreshToken = "refresh token";
            _mediatorMock.Setup(mediator =>
                    mediator.Send(It.Is<UserLoginCommand>(x =>
                        x.Email == email && x.Password == password), CancellationToken.None))
                .ReturnsAsync(new AuthResponse { Token = token, RefreshToken = refreshToken });

            var controller = new AuthController(_mapperMock.Object, _mediatorMock.Object);

            var result = await controller.Login(userLoginRequest);

            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var authSuccessResponse = Assert.IsType<AuthResponse>(okObjectResult.Value);
            Assert.Equal(token, authSuccessResponse.Token);
            Assert.Equal(refreshToken, authSuccessResponse.RefreshToken);
        }

        [Fact]
        public async Task GivenValidTokenAndRefreshToken_WhenRefreshingToken_ThenReturnsAuthSuccessResponse()
        {
            const string token = "token";
            const string refreshToken = "refresh token";
            var refreshTokenRequest = new RefreshTokenRequest
            {
                Token = token,
                RefreshToken = refreshToken
            };
            const string newToken = "new token";
            const string newRefreshToken = "new refresh token";

            _mediatorMock.Setup(mediator =>
                    mediator.Send(It.Is<RefreshTokenCommand>(x =>
                        x.Token == token && x.RefreshToken == refreshToken), CancellationToken.None))
                .ReturnsAsync(new AuthResponse { Token = newToken, RefreshToken = newRefreshToken });

            var controller = new AuthController(_mapperMock.Object, _mediatorMock.Object);

            var result = await controller.Refresh(refreshTokenRequest);

            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var authSuccessResponse = Assert.IsType<AuthResponse>(okObjectResult.Value);
            Assert.Equal(newToken, authSuccessResponse.Token);
            Assert.Equal(newRefreshToken, authSuccessResponse.RefreshToken);
        }
    }
}
