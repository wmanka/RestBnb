using MediatR;
using RestBnb.Core.Contracts.V1.Responses;
using RestBnb.Core.Enums;
using System;

namespace RestBnb.API.Application.Bookings.Commands
{
    public class UpdateBookingCommand : IRequest<BookingResponse>
    {
        public int Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public BookingState BookingState { get; set; }
        public DateTime CancellationDate { get; set; }

        public UpdateBookingCommand(int id)
        {
            Id = id;
        }
    }
}
