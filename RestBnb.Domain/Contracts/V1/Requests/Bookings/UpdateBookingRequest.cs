using RestBnb.Core.Enums;
using System;

namespace RestBnb.Core.Contracts.V1.Requests.Bookings
{
    public class UpdateBookingRequest
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public BookingState BookingState { get; set; }
    }
}
