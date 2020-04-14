using AutoMapper;
using MediatR;
using RestBnb.API.Application.Properties.Commands;
using RestBnb.API.Application.Properties.Responses;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace RestBnb.API.Application.Properties.Handlers
{
    public class CreatePropertyHandler : IRequestHandler<CreatePropertyCommand, PropertyResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPropertiesService _propertiesService;

        public CreatePropertyHandler(
            IMapper mapper,
            IPropertiesService propertiesService)
        {
            _mapper = mapper;
            _propertiesService = propertiesService;
        }

        public async Task<PropertyResponse> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            var property = _mapper.Map<Property>(request);

            await _propertiesService.CreatePropertyAsync(property);

            return _mapper.Map<PropertyResponse>(property);
        }
    }
}
