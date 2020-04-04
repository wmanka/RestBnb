﻿using RestBnb.Core.Enums;
using System;

namespace RestBnb.API.Contracts.V1.Responses
{
    public class BookingResponse
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal PricePerNight { get; set; }
        public decimal TotalPrice { get; set; }
        public BookingState BookingState { get; set; }
        public int UserId { get; set; }
        public int PropertyId { get; set; }
    }
}