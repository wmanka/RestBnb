using AutoMapper;
using RestBnb.API.Application.Bookings.Commands;
using RestBnb.API.Application.Bookings.Requests;
using RestBnb.Core.Contracts.V1.Requests.Queries;
using RestBnb.Core.Contracts.V1.Responses;
using RestBnb.Core.Entities;

namespace RestBnb.API.Mapping
{
    public class BookingMappingProfile : Profile
    {
        public BookingMappingProfile()
        {
            CreateMap<CreateBookingRequest, CreateBookingCommand>();
            CreateMap<UpdateBookingRequest, UpdateBookingCommand>();
            CreateMap<CreateBookingCommand, Booking>();
            CreateMap<Booking, BookingResponse>();
            CreateMap<GetAllBookingsRequestQueryString, GetAllBookingsFilter>();
            CreateMap<UpdateBookingCommand, Booking>();
        }
    }
}
