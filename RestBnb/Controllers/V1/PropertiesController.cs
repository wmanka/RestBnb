using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestBnb.API.Contracts.V1;
using RestBnb.API.Contracts.V1.Requests;
using RestBnb.API.Contracts.V1.Requests.Queries;
using RestBnb.API.Contracts.V1.Responses;
using RestBnb.API.Extensions;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestBnb.API.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPropertiesService _propertiesService;

        public PropertiesController(
            IPropertiesService propertiesService,
            IMapper mapper)
        {
            _propertiesService = propertiesService;
            _mapper = mapper;
        }

        [HttpPost(ApiRoutes.Properties.Create)]
        public async Task<IActionResult> Create(CreatePropertyRequest propertyRequest)
        {
            var property = _mapper.Map<Property>(propertyRequest);
            property.UserId = HttpContext.GetCurrentUserId();

            await _propertiesService.CreatePropertyAsync(property);

            return Ok(_mapper.Map<PropertyResponse>(property));
        }

        [HttpGet(ApiRoutes.Properties.GetAll)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllPropertiesQuery query)
        {
            var filter = _mapper.Map<GetAllPropertiesFilter>(query);

            var properties = await _propertiesService.GetAllPropertiesAsync(filter);

            return Ok(_mapper.Map<IEnumerable<PropertyResponse>>(properties));
        }

        [HttpDelete(ApiRoutes.Properties.Delete)]
        public async Task<IActionResult> Delete(int propertyId)
        {
            var userOwnsProperty =
                await _propertiesService.DoesUserOwnProperty(HttpContext.GetCurrentUserId(), propertyId);

            if (!userOwnsProperty)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = new List<ErrorModel>
                    {
                        new ErrorModel{ Message = "You do not own this post"}
                    }
                });
            }

            var deleted = await _propertiesService.DeletePropertyAsync(propertyId);

            if (deleted)
                return NoContent();

            return NotFound();
        }
    }
}