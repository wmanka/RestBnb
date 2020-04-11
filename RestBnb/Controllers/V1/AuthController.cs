using Microsoft.AspNetCore.Mvc;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Constants;
using RestBnb.Core.Contracts.V1.Requests.Auth;
using RestBnb.Core.Contracts.V1.Responses;
using System.Threading.Tasks;

namespace RestBnb.API.Controllers.V1
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost(ApiRoutes.Auth.Register)]
        public async Task<IActionResult> Register(UserRegistrationRequest request)
        {
            var authResponse = await _authService.RegisterAsync(request.Email, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }

        [HttpPost(ApiRoutes.Auth.Login)]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            var authResponse = await _authService.LoginAsync(request.Email, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }

        [HttpPost(ApiRoutes.Auth.Refresh)]
        public async Task<IActionResult> Refresh(RefreshTokenRequest request)
        {
            var authResponse = await _authService.RefreshTokenAsync(request.Token, request.RefreshToken);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }
    }
}