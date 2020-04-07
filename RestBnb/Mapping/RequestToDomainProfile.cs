using AutoMapper;
using RestBnb.Core.Contracts.V1.Requests.Bookings;
using RestBnb.Core.Contracts.V1.Requests.Properties;
using RestBnb.Core.Contracts.V1.Requests.Queries;
using RestBnb.Core.Entities;

namespace RestBnb.API.Mapping
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<PropertyRequest, Property>();
            CreateMap<GetAllPropertiesQuery, GetAllPropertiesFilter>();

            CreateMap<BookingCreateRequest, Booking>();
            CreateMap<BookingUpdateRequest, Booking>();
            CreateMap<GetAllBookingsQuery, GetAllBookingsFilter>();
        }
    }
}