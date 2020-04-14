using AutoMapper;
using RestBnb.API.Application.Properties.Commands;
using RestBnb.API.Application.Properties.Requests;
using RestBnb.API.Application.Properties.Requests.QueryStrings;
using RestBnb.API.Application.Properties.Responses;
using RestBnb.Core.Entities;

namespace RestBnb.API.Mapping
{
    public class PropertyMapping : Profile
    {
        public PropertyMapping()
        {
            CreateMap<CreatePropertyRequest, CreatePropertyCommand>();
            CreateMap<UpdatePropertyRequest, UpdatePropertyCommand>();
            CreateMap<CreatePropertyCommand, Property>();
            CreateMap<Property, PropertyResponse>();
            CreateMap<GetAllPropertiesRequestQueryString, GetAllPropertiesFilter>();
            CreateMap<UpdatePropertyCommand, Property>();
        }
    }
}
