using Microsoft.AspNetCore.Mvc;

namespace RestBnb.API.Application.Cities.Requests.QueryStrings
{
    public class GetAllCitiesRequestQueryString
    {
        [FromQuery(Name = "name")]
        public string Name { get; set; }
    }
}
