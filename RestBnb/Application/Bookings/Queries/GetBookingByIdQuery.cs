using MediatR;
using RestBnb.Core.Contracts.V1.Responses;

namespace RestBnb.API.Application.Bookings.Queries
{
    public class GetBookingByIdQuery : IRequest<BookingResponse>
    {
        public int Id { get; set; }

        public GetBookingByIdQuery(int id)
        {
            Id = id;
        }
    }
}
