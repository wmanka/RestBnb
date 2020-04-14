using AutoMapper;
using RestBnb.Core.Contracts.V1.Responses;
using RestBnb.Core.Entities;

namespace RestBnb.API.Mapping
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<Booking, BookingResponse>();
            CreateMap<User, UserResponse>();
        }
    }
}
