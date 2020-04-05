using AutoMapper;
using RestBnb.Core.Contracts.V1.Requests;
using RestBnb.Core.Entities;

namespace RestBnb.API.Mapping
{
    public class DomainToRequestProfile : Profile
    {
        public DomainToRequestProfile()
        {
            CreateMap<Booking, BookingCreateRequest>();
            CreateMap<Booking, BookingUpdateRequest>();
        }
    }
}
