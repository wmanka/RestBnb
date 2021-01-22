using AutoMapper;
using RestBnb.API.Application.PropertyImages.Responses;
using RestBnb.Core.Entities;

namespace RestBnb.API.Mapping
{
    public class PropertyImageMappingProfile : Profile
    {
        public PropertyImageMappingProfile()
        {
            CreateMap<PropertyImage, PropertyImageResponse>();
        }
    }
}
