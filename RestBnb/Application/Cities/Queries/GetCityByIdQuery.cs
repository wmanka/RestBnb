using MediatR;
using RestBnb.API.Application.Properties.Responses;

namespace RestBnb.API.Application.Cities.Queries
{
    public class GetCityByIdQuery : IRequest<CityResponse>
    {
        public int Id { get; set; }

        public GetCityByIdQuery(int id)
        {
            Id = id;
        }
    }
}
