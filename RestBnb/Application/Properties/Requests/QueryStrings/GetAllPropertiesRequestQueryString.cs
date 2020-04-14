using Microsoft.AspNetCore.Mvc;

namespace RestBnb.API.Application.Properties.Requests.QueryStrings
{
    public class GetAllPropertiesRequestQueryString
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