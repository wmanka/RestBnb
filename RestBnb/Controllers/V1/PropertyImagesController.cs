using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestBnb.API.Application.PropertyImages.Commands;
using RestBnb.API.Application.PropertyImages.Queries;
using RestBnb.API.Application.PropertyImages.Requests;
using RestBnb.Core.Constants;
using System.Threading.Tasks;

namespace RestBnb.API.Controllers.V1
{
    public class PropertyImagesController : BaseController
    {
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost(ApiRoutes.PropertyImages.Create)]
        public async Task<IActionResult> Create(int propertyId, CreatePropertyImageRequest createPropertyImageRequest)
        {
            var response = await Mediator.Send(Mapper.Map(createPropertyImageRequest, new CreatePropertyImageCommand(propertyId)));

            return Created(
                ApiRoutes.PropertyImages.GetAll
                .Replace("{propertyId}", propertyId.ToString())
                .Replace("{imageId}", response.Id.ToString()), response);
        }

        [HttpGet(ApiRoutes.PropertyImages.GetAll)]
        public async Task<IActionResult> GetAll(int propertyId)
        {
            var response = await Mediator.Send(new GetAllPropertyImagesQuery(propertyId));

            return Ok(response);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete(ApiRoutes.PropertyImages.Delete)]
        public async Task<IActionResult> Delete(int propertyId, int imageId)
        {
            await Mediator.Send(new DeletePropertyImageCommand(propertyId, imageId));

            return NoContent();
        }
    }
}
