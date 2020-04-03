using RestBnb.Core.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestBnb.Core.Entities
{
    public class Booking : BaseEntity
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal PricePerNight { get; set; }
        public decimal TotalPrice { get; set; }
        public BookingState BookingState { get; set; }
        public int UserId { get; set; }
        public int PropertyId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [ForeignKey(nameof(PropertyId))]
        public Property Property { get; set; }
    }
}
