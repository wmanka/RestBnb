using MediatR;
using RestBnb.API.Application.Properties.Responses;

namespace RestBnb.API.Application.Properties.Queries
{
    public class GetPropertyByIdQuery : IRequest<PropertyResponse>
    {
        public int Id { get; set; }

        public GetPropertyByIdQuery(int id)
        {
            Id = id;
        }
    }
}
