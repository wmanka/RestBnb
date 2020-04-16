using AutoMapper;
using RestBnb.Core.Contracts.V1.Requests.Bookings;
using RestBnb.Core.Contracts.V1.Requests.Queries;
using RestBnb.Core.Contracts.V1.Requests.Users;
using RestBnb.Core.Entities;

namespace RestBnb.API.Mapping
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<CreateBookingRequest, Booking>();
            CreateMap<UpdateBookingRequest, Booking>();
            CreateMap<GetAllBookingsRequestQueryString, GetAllBookingsFilter>();

            CreateMap<UserUpdateRequest, User>();
        }
    }
}