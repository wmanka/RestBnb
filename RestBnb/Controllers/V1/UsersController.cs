using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestBnb.API.Application.Users.Commands;
using RestBnb.API.Application.Users.Queries;
using RestBnb.API.Application.Users.Requests;
using RestBnb.Core.Constants;
using System.Threading.Tasks;

namespace RestBnb.API.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : BaseController
    {
        [HttpGet(ApiRoutes.Users.Get)]
        public async Task<IActionResult> Get(int userId)
        {
            var result = await Mediator.Send(new GetUserByIdQuery(userId));

            return Ok(result);
        }

        [HttpPut(ApiRoutes.Users.Update)]
        public async Task<IActionResult> Update(int userId, UpdateUserRequest request)
        {
            var response = await Mediator.Send(Mapper.Map(request, new UpdateUserCommand(userId)));

            return Ok(response);
        }

        [HttpDelete(ApiRoutes.Users.Delete)]
        public async Task<IActionResult> Delete(int userId)
        {
            await Mediator.Send(new DeleteUserCommand(userId));

            return NoContent();
        }
    }
}