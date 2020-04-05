using AutoMapper;
using RestBnb.Core.Contracts.V1.Requests.Properties;
using RestBnb.Core.Contracts.V1.Responses;
using RestBnb.Core.Entities;

namespace RestBnb.API.Mapping
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<Property, PropertyResponse>();
            CreateMap<Property, PropertyRequest>();

            CreateMap<Booking, BookingResponse>();
        }
    }
}
