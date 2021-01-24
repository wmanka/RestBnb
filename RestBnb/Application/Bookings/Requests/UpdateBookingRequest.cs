using RestBnb.Core.Enums;
using System;

namespace RestBnb.API.Application.Bookings.Requests
{
    public class UpdateBookingRequest
    {
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public BookingState? BookingState { get; set; }
        public DateTime? CancellationDate { get; set; }
    }
}
