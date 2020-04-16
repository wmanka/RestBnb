using AutoMapper;
using MediatR;
using RestBnb.API.Application.Bookings.Queries;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Contracts.V1.Responses;
using RestBnb.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RestBnb.API.Application.Bookings.Handlers
{
    public class GetAllBookingsHandler : IRequestHandler<GetAllBookingsQuery, IEnumerable<BookingResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IBookingsService _bookingsService;

        public GetAllBookingsHandler(IBookingsService bookingsService, IMapper mapper)
        {
            _bookingsService = bookingsService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookingResponse>> Handle(GetAllBookingsQuery request, CancellationToken cancellationToken)
        {
            var filter = _mapper.Map<GetAllBookingsFilter>(request.BookingsRequestQueryString);
            var properties = await _bookingsService.GetAllBookingsAsync(filter);

            return _mapper.Map<IEnumerable<BookingResponse>>(properties);
        }
    }
}
