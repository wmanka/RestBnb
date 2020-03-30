﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestBnb.Core.Entities
{
    public class Property : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public decimal PricePerNight { get; set; }
        public int BedroomNumber { get; set; }
        public int BathroomNumber { get; set; }
        public int AccommodatesNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }
        public User User { get; set; }

        [ForeignKey(nameof(UserId))]
        public int UserId { get; set; }
    }
}