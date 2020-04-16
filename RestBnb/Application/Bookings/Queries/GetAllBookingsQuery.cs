using MediatR;
using RestBnb.Core.Contracts.V1.Requests.Queries;
using RestBnb.Core.Contracts.V1.Responses;
using System.Collections.Generic;

namespace RestBnb.API.Application.Bookings.Queries
{
    public class GetAllBookingsQuery : IRequest<IEnumerable<BookingResponse>>
    {
        public GetAllBookingsRequestQueryString BookingsRequestQueryString { get; set; }

        public GetAllBookingsQuery(GetAllBookingsRequestQueryString bookingsRequestQueryString)
        {
            BookingsRequestQueryString = bookingsRequestQueryString;
        }
    }
}
