using Microsoft.AspNetCore.Mvc;
using RestBnb.API.Application.Auth.Commands;
using RestBnb.API.Application.Auth.Requests;
using RestBnb.Core.Constants;
using System.Threading.Tasks;

namespace RestBnb.API.Controllers.V1
{
    public class AuthController : BaseController
    {
        [HttpPost(ApiRoutes.Auth.Register)]
        public async Task<IActionResult> Register(UserRegistrationRequest request)
        {
            var response = await Mediator.Send(new UserRegistrationCommand(request.Email, request.Password));

            return Ok(response);
        }

        [HttpPost(ApiRoutes.Auth.Login)]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            var response = await Mediator.Send(new UserLoginCommand(request.Email, request.Password));

            return Ok(response);
        }

        [HttpPost(ApiRoutes.Auth.Refresh)]
        public async Task<IActionResult> Refresh(RefreshTokenRequest request)
        {
            var response = await Mediator.Send(new RefreshTokenCommand(request.Token, request.RefreshToken));

            return Ok(response);
        }
    }
}