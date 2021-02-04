using RestBnb.Core.Enums;
using System;

namespace RestBnb.Core.Entities
{
    public class GetAllBookingsFilter
    {
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public BookingState? BookingState { get; set; }
        public int? UserId { get; set; }
        public int? PropertyId { get; set; }
    }
}
