using System;

namespace RestBnb.API.Application.Properties.Requests
{
    public class UpdatePropertyRequest
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
    }
}
