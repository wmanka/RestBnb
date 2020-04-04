﻿using AutoMapper;
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
        public async Task<IActionResult> Create(BookingRequest bookingRequest)
        {
            var booking = _mapper.Map<Booking>(bookingRequest);
            booking.UserId = HttpContext.GetCurrentUserId();

            await _bookingsService.CreateBookingAsync(booking);

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
            var userOwnsBooking =
                await _bookingsService.DoesUserOwnBookingAsync(HttpContext.GetCurrentUserId(), bookingId);

            if (!userOwnsBooking)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = new List<ErrorModel>
                    {
                        new ErrorModel{ Message = "You do not own this booking"}
                    }
                });
            }

            var deleted = await _bookingsService.DeleteBookingAsync(bookingId);

            if (deleted)
                return NoContent();

            return NotFound();
        }

        [HttpPut(ApiRoutes.Bookings.Put)]
        public async Task<IActionResult> Put(int bookingId, BookingRequest bookingRequest)
        {
            var userOwnsBooking =
                await _bookingsService.DoesUserOwnBookingAsync(HttpContext.GetCurrentUserId(), bookingId);

            if (!userOwnsBooking)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = new List<ErrorModel>
                    {
                        new ErrorModel{ Message = "You do not own this booking"}
                    }
                });
            }

            var bookingFromDb = await _bookingsService.GetBookingByIdAsync(bookingId);

            if (bookingFromDb == null)
                return NotFound();

            _mapper.Map(bookingRequest, bookingFromDb);

            await _bookingsService.UpdateBookingAsync(bookingFromDb);

            return Ok(_mapper.Map<BookingResponse>(bookingFromDb));
        }

        [HttpPatch(ApiRoutes.Bookings.Patch)]
        public async Task<IActionResult> Patch(int bookingId, JsonPatchDocument<BookingRequest> bookingRequestPatchModel)
        {
            var userOwnsBooking =
                await _bookingsService.DoesUserOwnBookingAsync(HttpContext.GetCurrentUserId(), bookingId);

            if (!userOwnsBooking)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = new List<ErrorModel>
                    {
                        new ErrorModel{ Message = "You do not own this booking"}
                    }
                });
            }

            var bookingFromDb = await _bookingsService.GetBookingByIdAsync(bookingId);
            if (bookingFromDb == null) return NotFound();

            var bookingFromDbDto = _mapper.Map<Booking, BookingRequest>(bookingFromDb);

            bookingRequestPatchModel.ApplyTo(bookingFromDbDto);
            _mapper.Map(bookingFromDbDto, bookingFromDb);

            await _bookingsService.UpdateBookingAsync(bookingFromDb);

            return Ok(_mapper.Map<Booking, BookingResponse>(bookingFromDb));
        }
    }
}