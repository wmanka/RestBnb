using MediatR;
using RestBnb.API.Application.Cities.Requests.QueryStrings;
using RestBnb.API.Application.Properties.Responses;
using System.Collections.Generic;

namespace RestBnb.API.Application.Cities.Queries
{
    public class GetAllCitiesQuery : IRequest<IEnumerable<CityResponse>>
    {
        public GetAllCitiesRequestQueryString PropertiesRequestQueryString { get; set; }

        public GetAllCitiesQuery(GetAllCitiesRequestQueryString citiesRequestQueryString)
        {
            PropertiesRequestQueryString = citiesRequestQueryString;
        }
    }
}
