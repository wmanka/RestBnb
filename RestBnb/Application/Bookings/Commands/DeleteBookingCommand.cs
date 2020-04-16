using MediatR;
using RestBnb.Core.Contracts.V1.Responses;

namespace RestBnb.API.Application.Bookings.Commands
{
    public class DeleteBookingCommand : IRequest<BookingResponse>
    {
        public int Id { get; set; }

        public DeleteBookingCommand(int id)
        {
            Id = id;
        }
    }
}
