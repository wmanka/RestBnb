using AutoMapper;
using MediatR;
using RestBnb.API.Application.Cities.Queries;
using RestBnb.API.Application.Properties.Responses;
using RestBnb.API.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RestBnb.API.Application.Cities.Handlers
{
    public class GetCityByIdHandler : IRequestHandler<GetCityByIdQuery, CityResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICitiesService _citiesService;

        public GetCityByIdHandler(ICitiesService propertiesService, IMapper mapper)
        {
            _citiesService = propertiesService;
            _mapper = mapper;
        }

        public async Task<CityResponse> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
        {
            var city = await _citiesService.GetCityByIdAsync(request.Id);

            return _mapper.Map<CityResponse>(city);
        }
    }
}