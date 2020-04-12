using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Constants;
using RestBnb.Core.Contracts.V1.Requests.Users;
using RestBnb.Core.Contracts.V1.Responses;
using RestBnb.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestBnb.API.Controllers.V1
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;

        public UsersController(
            IMapper mapper,
            IUsersService usersService)
        {
            _mapper = mapper;
            _usersService = usersService;
        }

        [HttpGet(ApiRoutes.Users.Get)]
        public async Task<IActionResult> Get(int userId)
        {
            var user = await _usersService.GetUserByIdAsync(userId);

            if (user == null)
                return NotFound();

            return Ok(_mapper.Map<User, UserResponse>(user));
        }

        [HttpPost(ApiRoutes.Users.Patch)]
        public async Task<IActionResult> Update(int userId, JsonPatchDocument<UserUpdateRequest> patchRequest)
        {
            var user = await _usersService.GetUserByIdAsync(userId);

            if (user == null)
                return NotFound();

            var userMappedToRequest = _mapper.Map<User, UserUpdateRequest>(user);
            patchRequest.ApplyTo(userMappedToRequest);
            _mapper.Map(userMappedToRequest, user);

            if (!await _usersService.UpdateUserAsync(user))
            {
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Could not update user" } } });
            }

            return Ok(_mapper.Map<User, UserResponse>(user));
        }

        [HttpDelete(ApiRoutes.Users.Delete)]
        public async Task<IActionResult> Delete(int userId)
        {
            var deleted = await _usersService.DeleteUserAsync(userId);

            if (deleted)
                return NoContent();

            return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Could not delete user" } } });
        }
    }
}