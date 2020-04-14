using AutoMapper;
using MediatR;
using RestBnb.API.Application.Properties.Queries;
using RestBnb.API.Application.Properties.Responses;
using RestBnb.API.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RestBnb.API.Application.Properties.Handlers
{
    public class GetPropertyByIdHandler : IRequestHandler<GetPropertyByIdQuery, PropertyResponse>
    {
        private readonly IPropertiesService _propertiesService;
        private readonly IMapper _mapper;

        public GetPropertyByIdHandler(
            IPropertiesService propertiesService,
            IMapper mapper)
        {
            _propertiesService = propertiesService;
            _mapper = mapper;
        }

        public async Task<PropertyResponse> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
        {
            var property = await _propertiesService.GetPropertyByIdAsync(request.Id);

            return _mapper.Map<PropertyResponse>(property);
        }
    }
}
