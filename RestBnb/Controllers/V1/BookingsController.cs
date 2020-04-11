using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Constants;
using RestBnb.Core.Contracts.V1.Requests.Bookings;
using RestBnb.Core.Contracts.V1.Requests.Queries;
using RestBnb.Core.Contracts.V1.Responses;
using RestBnb.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestBnb.API.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBookingsService _bookingsService;

        public BookingsController(
            IBookingsService bookingsService,
            IMapper mapper)
        {
            _bookingsService = bookingsService;
            _mapper = mapper;
        }

        [HttpPost(ApiRoutes.Bookings.Create)]
        public async Task<IActionResult> Create(BookingCreateRequest request)
        {
            var booking = _mapper.Map<Booking>(request);

            if (!await _bookingsService.CreateBookingAsync(booking))
            {
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Could not create new booking" } } });
            }

            return Created(
                ApiRoutes.Bookings.Get.Replace("{bookingId}", booking.Id.ToString()),
                _mapper.Map<BookingResponse>(booking));
        }

        [HttpGet(ApiRoutes.Bookings.GetAll)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllBookingsQuery query)
        {
            var filter = _mapper.Map<GetAllBookingsFilter>(query);

            var bookings = await _bookingsService.GetAllBookingsAsync(filter);

            return Ok(_mapper.Map<IEnumerable<BookingResponse>>(bookings));
        }

        [HttpGet(ApiRoutes.Bookings.Get)]
        public async Task<IActionResult> Get(int bookingId)
        {
            var booking = await _bookingsService.GetBookingByIdAsync(bookingId);

            return Ok(_mapper.Map<BookingResponse>(booking));
        }

        [HttpDelete(ApiRoutes.Bookings.Delete)]
        public async Task<IActionResult> Delete(int bookingId)
        {
            var deleted = await _bookingsService.DeleteBookingAsync(bookingId);

            if (deleted)
                return NoContent();

            return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Could not delete booking" } } });
        }

        [HttpPut(ApiRoutes.Bookings.Put)]
        public async Task<IActionResult> Put(int bookingId, BookingUpdateRequest request)
        {
            var booking = await _bookingsService.GetBookingByIdAsync(bookingId);

            if (booking == null)
                return NotFound();

            _mapper.Map(request, booking);

            if (!await _bookingsService.UpdateBookingAsync(booking))
            {
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Could not update booking" } } });
            }

            return Ok(_mapper.Map<BookingResponse>(booking));
        }

        [HttpPatch(ApiRoutes.Bookings.Patch)]
        public async Task<IActionResult> Patch(int bookingId, JsonPatchDocument<BookingUpdateRequest> patchRequest)
        {
            var booking = await _bookingsService.GetBookingByIdAsync(bookingId);

            if (booking == null)
                return NotFound();

            var bookingMappedToRequest = _mapper.Map<Booking, BookingUpdateRequest>(booking);

            patchRequest.ApplyTo(bookingMappedToRequest);
            _mapper.Map(bookingMappedToRequest, booking);

            if (!await _bookingsService.UpdateBookingAsync(booking))
            {
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Could not patch booking" } } });
            }

            return Ok(_mapper.Map<Booking, BookingResponse>(booking));
        }
    }
}