using AutoMapper;
using RestBnb.API.Application.PropertyImages.Commands;
using RestBnb.API.Application.PropertyImages.Requests;
using RestBnb.API.Application.PropertyImages.Responses;
using RestBnb.Core.Entities;

namespace RestBnb.API.Mapping
{
    public class PropertyImageMappingProfile : Profile
    {
        public PropertyImageMappingProfile()
        {
            CreateMap<CreatePropertyImageRequest, CreatePropertyImageCommand>();
            CreateMap<CreatePropertyImageCommand, PropertyImage>();
            CreateMap<PropertyImage, PropertyImageResponse>();
        }
    }
}
