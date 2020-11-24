using AutoMapper;
using RestBnb.API.Application.Cities.Requests.QueryStrings;
using RestBnb.API.Application.Properties.Responses;
using RestBnb.Core.Entities;

namespace RestBnb.API.Mapping
{
    public class CityMappingProfile : Profile
    {
        public CityMappingProfile()
        {
            CreateMap<City, CityResponse>();
            CreateMap<GetAllCitiesRequestQueryString, GetAllCitiesFilter>();
        }
    }
}
