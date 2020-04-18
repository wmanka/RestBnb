using MediatR;
using RestBnb.Core.Contracts.V1.Responses;
using System;

namespace RestBnb.API.Application.Bookings.Commands
{
    public class CreateBookingCommand : IRequest<BookingResponse>
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int PropertyId { get; set; }
    }
}
