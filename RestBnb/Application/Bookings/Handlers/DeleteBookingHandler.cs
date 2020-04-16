using MediatR;
using RestBnb.API.Application.Bookings.Commands;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Contracts.V1.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace RestBnb.API.Application.Bookings.Handlers
{
    public class DeleteBookingHandler : IRequestHandler<DeleteBookingCommand, BookingResponse>
    {
        private readonly IBookingsService _bookingsService;

        public DeleteBookingHandler(
            IBookingsService bookingsService)
        {
            _bookingsService = bookingsService;
        }

        public async Task<BookingResponse> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
        {
            await _bookingsService.DeleteBookingAsync(request.Id);
            return null;
        }
    }
}
