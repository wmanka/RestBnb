using AutoMapper;
using MediatR;
using RestBnb.API.Application.Cities.Queries;
using RestBnb.API.Application.Properties.Responses;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RestBnb.API.Application.Cities.Handlers
{
    public class GetAllCitiesHandler : IRequestHandler<GetAllCitiesQuery, IEnumerable<CityResponse>>
    {
        private readonly IMapper _mapper;
        private readonly ICitiesService _citiesService;

        public GetAllCitiesHandler(ICitiesService propertiesService, IMapper mapper)
        {
            _citiesService = propertiesService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CityResponse>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            var filter = _mapper.Map<GetAllCitiesFilter>(request.PropertiesRequestQueryString);
            var cities = await _citiesService.GetAllCitiesAsync(filter);

            return _mapper.Map<IEnumerable<CityResponse>>(cities);
        }
    }
}
