using AutoMapper;
using RestBnb.API.Contracts.V1.Responses;
using RestBnb.Core.Entities;

namespace RestBnb.API.Mapping
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<Property, PropertyResponse>();
        }
    }
}
