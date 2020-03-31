using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
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
        public async Task<IActionResult> Create(PropertyRequest propertyRequest)
        {
            var property = _mapper.Map<Property>(propertyRequest);
            property.UserId = HttpContext.GetCurrentUserId();

            await _propertiesService.CreatePropertyAsync(property);

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
            var userOwnsProperty =
                await _propertiesService.DoesUserOwnPropertyAsync(HttpContext.GetCurrentUserId(), propertyId);

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

        [HttpPut(ApiRoutes.Properties.Put)]
        public async Task<IActionResult> Put(int propertyId, PropertyRequest propertyRequest)
        {
            var userOwnsProperty =
                await _propertiesService.DoesUserOwnPropertyAsync(HttpContext.GetCurrentUserId(), propertyId);

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

            var propertyFromDb = await _propertiesService.GetPropertyByIdAsync(propertyId);

            if (propertyFromDb == null)
                return NotFound();

            _mapper.Map(propertyRequest, propertyFromDb);

            await _propertiesService.UpdatePropertyAsync(propertyFromDb);

            return Ok(_mapper.Map<PropertyResponse>(propertyFromDb));
        }

        [HttpPatch(ApiRoutes.Properties.Patch)]
        public async Task<IActionResult> Patch(int propertyId, JsonPatchDocument<PropertyRequest> propertyRequestPatchModel)
        {
            // request model as [{ "op" : "replace", "path" : "pricePerNight", "value" : 220}]

            var userOwnsProperty =
                await _propertiesService.DoesUserOwnPropertyAsync(HttpContext.GetCurrentUserId(), propertyId);

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

            var propertyFromDb = await _propertiesService.GetPropertyByIdAsync(propertyId);
            if (propertyFromDb == null) return NotFound();

            var propertyFromDbDto = _mapper.Map<Property, PropertyRequest>(propertyFromDb);

            propertyRequestPatchModel.ApplyTo(propertyFromDbDto);
            _mapper.Map(propertyFromDbDto, propertyFromDb);

            await _propertiesService.UpdatePropertyAsync(propertyFromDb);

            return Ok(_mapper.Map<Property, PropertyResponse>(propertyFromDb));
        }
    }
}