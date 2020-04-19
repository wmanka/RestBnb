using AutoMapper;
using MediatR;
using RestBnb.API.Application.Bookings.Commands;
using RestBnb.API.Services;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Contracts.V1.Responses;
using RestBnb.Core.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace RestBnb.API.Application.Bookings.Handlers
{
    public class CreateBookingHandler : IRequestHandler<CreateBookingCommand, BookingResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBookingsService _bookingsService;
        private readonly IPropertiesService _propertiesService;
        private readonly UserResolverService _userResolverService;

        public CreateBookingHandler(
                IMapper mapper,
                IBookingsService bookingsService,
                UserResolverService userResolverService,
                IPropertiesService propertiesService)
        {
            _mapper = mapper;
            _bookingsService = bookingsService;
            _userResolverService = userResolverService;
            _propertiesService = propertiesService;
        }

        public async Task<BookingResponse> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = _mapper.Map<Booking>(request);

            var property = await _propertiesService.GetPropertyByIdAsync(booking.PropertyId);

            booking.UserId = _userResolverService.GetUserId();
            booking.PricePerNight = property.PricePerNight;
            booking.TotalPrice = (booking.CheckOutDate - booking.CheckInDate).Days * booking.PricePerNight;

            await _bookingsService.CreateBookingAsync(booking);

            return _mapper.Map<BookingResponse>(booking);
        }
    }
}
