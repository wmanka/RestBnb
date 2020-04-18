using System;

namespace RestBnb.API.Application.Bookings.Requests
{
    public class CreateBookingRequest
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int PropertyId { get; set; }
    }
}
