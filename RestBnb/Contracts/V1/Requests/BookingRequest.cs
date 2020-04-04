using System;

namespace RestBnb.API.Contracts.V1.Requests
{
    public class BookingRequest
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal PricePerNight { get; set; }
        public int PropertyId { get; set; }
    }
}
