﻿using System;

namespace RestBnb.Core.Contracts.V1.Requests.Bookings
{
    public class BookingCreateRequest
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal PricePerNight { get; set; }
        public int PropertyId { get; set; }
    }
}