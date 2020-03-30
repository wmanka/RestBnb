using Microsoft.AspNetCore.Mvc;

namespace RestBnb.Core.Contracts.V1.Requests.Queries
{
    public class GetAllPropertiesQuery
    {
        [FromQuery(Name = "maxPricePerNight")]
        public decimal MaxPricePerNight { get; set; }

        [FromQuery(Name = "minPricePerNight")]
        public decimal MinPricePerNight { get; set; }

        [FromQuery(Name = "accommodatesNumber")]
        public decimal AccommodatesNumber { get; set; }

        [FromQuery(Name = "userId")]
        public decimal UserId { get; set; }
    }
}