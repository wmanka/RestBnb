using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestBnb.API.Application.Bookings.Commands;
using RestBnb.API.Application.Bookings.Queries;
using RestBnb.API.Application.Bookings.Requests;
using RestBnb.Core.Constants;
using RestBnb.Core.Contracts.V1.Requests.Queries;
using RestBnb.Core.Contracts.V1.Responses;
using System.Threading.Tasks;

namespace RestBnb.API.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BookingsController : BaseController
    {
        public BookingsController(IMapper mapper, IMediator mediator) : base(mapper, mediator) { }

        [HttpPost(ApiRoutes.Bookings.Create)]
        public async Task<IActionResult> Create(CreateBookingRequest createBookingRequest)
        {
            var response = await Mediator.Send(Mapper.Map<CreateBookingCommand>(createBookingRequest));

            return Created(
                ApiRoutes.Bookings.Get.Replace("{bookingId}", response.Id.ToString()),
                Mapper.Map<BookingResponse>(response));
        }

        [HttpGet(ApiRoutes.Bookings.GetAll)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllBookingsRequestQueryString requestQueryString)
        {
            var response = await Mediator.Send(new GetAllBookingsQuery(requestQueryString));

            return Ok(response);
        }

        [HttpGet(ApiRoutes.Bookings.Get)]
        public async Task<IActionResult> Get(int bookingId)
        {
            var response = await Mediator.Send(new GetBookingByIdQuery(bookingId));
            return Ok(response);
        }

        [HttpDelete(ApiRoutes.Bookings.Delete)]
        public async Task<IActionResult> Delete(int bookingId)
        {
            await Mediator.Send(new DeleteBookingCommand(bookingId));

            return NoContent();
        }

        [HttpPut(ApiRoutes.Bookings.Update)]
        public async Task<IActionResult> Update(int bookingId, UpdateBookingRequest request)
        {
            var response = await Mediator.Send(Mapper.Map(request, new UpdateBookingCommand(bookingId)));

            return Ok(response);
        }
    }
}