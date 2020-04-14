using AutoMapper;
using MediatR;
using RestBnb.API.Application.Properties.Queries;
using RestBnb.API.Application.Properties.Responses;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RestBnb.API.Application.Properties.Handlers
{
    public class GetAllPropertiesHandler : IRequestHandler<GetAllPropertiesQuery, IEnumerable<PropertyResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IPropertiesService _propertiesService;

        public GetAllPropertiesHandler(IPropertiesService propertiesService, IMapper mapper)
        {
            _propertiesService = propertiesService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PropertyResponse>> Handle(GetAllPropertiesQuery request, CancellationToken cancellationToken)
        {
            var filter = _mapper.Map<GetAllPropertiesFilter>(request.PropertiesRequestQueryString);
            var properties = await _propertiesService.GetAllPropertiesAsync(filter);

            return _mapper.Map<IEnumerable<PropertyResponse>>(properties);
        }
    }
}
