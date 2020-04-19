using AutoMapper;
using MediatR;
using RestBnb.API.Application.Bookings.Queries;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Contracts.V1.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace RestBnb.API.Application.Bookings.Handlers
{
    public class GetBookingByIdHandler : IRequestHandler<GetBookingByIdQuery, BookingResponse>
    {
        private readonly IBookingsService _bookingsService;
        private readonly IMapper _mapper;

        public GetBookingByIdHandler(
            IBookingsService bookingsService,
            IMapper mapper)
        {
            _bookingsService = bookingsService;
            _mapper = mapper;
        }

        public async Task<BookingResponse> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
        {
            var property = await _bookingsService.GetBookingByIdAsync(request.Id);

            return _mapper.Map<BookingResponse>(property);
        }
    }
}
