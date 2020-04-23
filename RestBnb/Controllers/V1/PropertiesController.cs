using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestBnb.API.Application.Properties.Commands;
using RestBnb.API.Application.Properties.Queries;
using RestBnb.API.Application.Properties.Requests;
using RestBnb.API.Application.Properties.Requests.QueryStrings;
using RestBnb.Core.Constants;
using System.Threading.Tasks;

namespace RestBnb.API.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PropertiesController : BaseController
    {
        [HttpPost(ApiRoutes.Properties.Create)]
        public async Task<IActionResult> Create(CreatePropertyRequest createPropertyRequest)
        {
            var response = await Mediator.Send(Mapper.Map<CreatePropertyCommand>(createPropertyRequest));

            return Created(
                ApiRoutes.Properties.Get.Replace("{propertyId}",
                response.Id.ToString()), response);
        }

        [HttpGet(ApiRoutes.Properties.GetAll)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllPropertiesRequestQueryString requestQueryString)
        {
            var response = await Mediator.Send(new GetAllPropertiesQuery(requestQueryString));

            return Ok(response);
        }

        [HttpGet(ApiRoutes.Properties.Get)]
        public async Task<IActionResult> GetById(int propertyId)
        {
            var response = await Mediator.Send(new GetPropertyByIdQuery(propertyId));

            return Ok(response);
        }

        [HttpDelete(ApiRoutes.Properties.Delete)]
        public async Task<IActionResult> Delete(int propertyId)
        {
            await Mediator.Send(new DeletePropertyCommand(propertyId));

            return NoContent();
        }

        [HttpPut(ApiRoutes.Properties.Update)]
        public async Task<IActionResult> Update(int propertyId, UpdatePropertyRequest request)
        {
            var response = await Mediator.Send(Mapper.Map(request, new UpdatePropertyCommand(propertyId)));

            return Ok(response);
        }
    }
}