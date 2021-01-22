using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RestBnb.API.Application.PropertyImages.Commands;
using RestBnb.API.Application.PropertyImages.Queries;
using RestBnb.Core.Constants;
using System.Linq;
using System.Threading.Tasks;

namespace RestBnb.API.Controllers.V1
{
    public class PropertyImagesController : BaseController
    {
        [EnableCors]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [RequestFormSizeLimit(valueCountLimit: 214748364)]
        [HttpPost(ApiRoutes.PropertyImages.Create)]
        public async Task<IActionResult> CreateRangeAsync(int propertyId)
        {
            var imagesCollection = await Request.ReadFormAsync();
            var images = imagesCollection.Files.ToArray();

            var response = await Mediator.Send(new CreatePropertyImageRangeCommand(propertyId, images));

            return Ok(response);
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
