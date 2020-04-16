using AutoMapper;
using MediatR;
using RestBnb.API.Application.Bookings.Commands;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Contracts.V1.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace RestBnb.API.Application.Bookings.Handlers
{
    public class UpdateBookingHandler : IRequestHandler<UpdateBookingCommand, BookingResponse>
    {
        private readonly IBookingsService _bookingsService;
        private readonly IMapper _mapper;

        public UpdateBookingHandler(
            IBookingsService bookingsService,
            IMapper mapper)
        {
            _bookingsService = bookingsService;
            _mapper = mapper;
        }

        public async Task<BookingResponse> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await _bookingsService.GetBookingByIdAsync(request.Id);

            _mapper.Map(request, booking);

            booking.TotalPrice = (booking.CheckOutDate - booking.CheckInDate).Days * booking.PricePerNight;

            await _bookingsService.UpdateBookingAsync(booking);

            return _mapper.Map<BookingResponse>(booking);
        }
    }
}
