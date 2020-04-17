using AutoMapper;
using MediatR;
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
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public PropertiesController(
            IMapper mapper,
            IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost(ApiRoutes.Properties.Create)]
        public async Task<IActionResult> Create(CreatePropertyRequest createPropertyRequest)
        {
            var response = await _mediator.Send(_mapper.Map<CreatePropertyCommand>(createPropertyRequest));

            return Created(
                ApiRoutes.Properties.Get.Replace("{propertyId}",
                response.Id.ToString()), response);
        }

        [HttpGet(ApiRoutes.Properties.GetAll)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllPropertiesRequestQueryString requestQueryString)
        {
            var response = await _mediator.Send(new GetAllPropertiesQuery(requestQueryString));

            return Ok(response);
        }

        [HttpGet(ApiRoutes.Properties.Get)]
        public async Task<IActionResult> GetById(int propertyId)
        {
            var response = await _mediator.Send(new GetPropertyByIdQuery(propertyId));

            return Ok(response);
        }

        [HttpDelete(ApiRoutes.Properties.Delete)]
        public async Task<IActionResult> Delete(int propertyId)
        {
            await _mediator.Send(new DeletePropertyCommand(propertyId));

            return NoContent();
        }

        [HttpPut(ApiRoutes.Properties.Update)]
        public async Task<IActionResult> Update(int propertyId, UpdatePropertyRequest request)
        {
            var response = await _mediator.Send(_mapper.Map(request, new UpdatePropertyCommand(propertyId)));

            return Ok(response);
        }
    }
}