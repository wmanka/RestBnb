using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Constants;
using RestBnb.Core.Contracts.V1.Requests.Properties;
using RestBnb.Core.Contracts.V1.Requests.Queries;
using RestBnb.Core.Contracts.V1.Responses;
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
        public async Task<IActionResult> Create(PropertyRequest propertyRequest)
        {
            var property = _mapper.Map<Property>(propertyRequest);

            if (!await _propertiesService.CreatePropertyAsync(property))
            {
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Could not create new property" } } });
            }

            return Created(
                ApiRoutes.Properties.Get.Replace("{propertyId}", property.Id.ToString()),
                _mapper.Map<PropertyResponse>(property));
        }

        [HttpGet(ApiRoutes.Properties.GetAll)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllPropertiesQuery query)
        {
            var filter = _mapper.Map<GetAllPropertiesFilter>(query);

            var properties = await _propertiesService.GetAllPropertiesAsync(filter);

            return Ok(_mapper.Map<IEnumerable<PropertyResponse>>(properties));
        }

        [HttpGet(ApiRoutes.Properties.Get)]
        public async Task<IActionResult> Get(int propertyId)
        {
            var property = await _propertiesService.GetPropertyByIdAsync(propertyId);

            return Ok(_mapper.Map<PropertyResponse>(property));
        }

        [HttpDelete(ApiRoutes.Properties.Delete)]
        public async Task<IActionResult> Delete(int propertyId)
        {
            var deleted = await _propertiesService.DeletePropertyAsync(propertyId);

            if (deleted)
                return NoContent();

            return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Could not delete property" } } });
        }

        [HttpPut(ApiRoutes.Properties.Put)]
        public async Task<IActionResult> Put(int propertyId, PropertyRequest propertyRequest)
        {
            var property = await _propertiesService.GetPropertyByIdAsync(propertyId);

            if (property == null)
                return NotFound();

            _mapper.Map(propertyRequest, property);

            if (!await _propertiesService.UpdatePropertyAsync(property))
            {
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Could not patch property" } } });
            }

            return Ok(_mapper.Map<PropertyResponse>(property));
        }

        [HttpPatch(ApiRoutes.Properties.Patch)]
        public async Task<IActionResult> Patch(int propertyId, JsonPatchDocument<PropertyRequest> patchRequest)
        {
            var property = await _propertiesService.GetPropertyByIdAsync(propertyId);

            if (property == null)
                return NotFound();

            var propertyMappedToRequest = _mapper.Map<Property, PropertyRequest>(property);

            patchRequest.ApplyTo(propertyMappedToRequest);
            _mapper.Map(propertyMappedToRequest, property);

            if (!await _propertiesService.UpdatePropertyAsync(property))
            {
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Could not patch property" } } });
            }

            return Ok(_mapper.Map<Property, PropertyResponse>(property));
        }
    }
}