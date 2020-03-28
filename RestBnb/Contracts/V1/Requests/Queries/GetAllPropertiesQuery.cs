using Microsoft.AspNetCore.Mvc;

namespace RestBnb.API.Contracts.V1.Requests.Queries
{
    public class GetAllPropertiesQuery
    {
        [FromQuery(Name = "maxPricePerNight")]
        public decimal MaxPricePerNight { get; set; }
    }
}