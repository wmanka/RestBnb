using System;
using System.Collections.Generic;
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
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public int CityId { get; set; }
        public int UserId { get; set; }

        [ForeignKey(nameof(CityId))]
        public City City { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public IEnumerable<Booking> Bookings { get; set; }
        public IEnumerable<PropertyImage> PropertyImages { get; set; }
    }
}