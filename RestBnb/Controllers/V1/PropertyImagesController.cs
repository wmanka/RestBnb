using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RestBnb.API.Application.PropertyImages.Commands;
using RestBnb.API.Application.PropertyImages.Queries;
using RestBnb.API.Application.PropertyImages.Requests;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Constants;
using RestBnb.Core.Entities;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RestBnb.API.Controllers.V1
{
    public class PropertyImagesController : BaseController
    {
        public IPropertyImagesService Service { get; }

        public PropertyImagesController(IPropertyImagesService service)
        {
            Service = service;
        }

        [EnableCors]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [RequestFormSizeLimit(valueCountLimit: 214748364)]
        [HttpPost(ApiRoutes.PropertyImages.Create)]
        public async Task<IActionResult> CreateRangeAsync(int propertyId)
        {
            var formCollection = await Request.ReadFormAsync();
            var files = formCollection.Files;

            var createPropertyImageRequest = new CreatePropertyImageRangeRequest();

            foreach (var file in files)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                await file.CopyToAsync(memoryStream);
                
                var propertyImage = new PropertyImage()
                {
                    Image = memoryStream.ToArray(),
                    PropertyId = propertyId
                };
                
                await Service.CreateAsync(propertyImage);
            }

            var response = await Mediator.Send(Mapper.Map(createPropertyImageRequest, new CreatePropertyImageRangeCommand(propertyId)));

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
