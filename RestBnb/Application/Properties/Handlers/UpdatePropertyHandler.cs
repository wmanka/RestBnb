using AutoMapper;
using MediatR;
using RestBnb.API.Application.Properties.Commands;
using RestBnb.API.Application.Properties.Responses;
using RestBnb.API.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RestBnb.API.Application.Properties.Handlers
{
    public class UpdatePropertyHandler : IRequestHandler<UpdatePropertyCommand, PropertyResponse>
    {
        private readonly IPropertiesService _propertiesService;
        private readonly IMapper _mapper;

        public UpdatePropertyHandler(
            IPropertiesService propertiesService,
            IMapper mapper)
        {
            _propertiesService = propertiesService;
            _mapper = mapper;
        }

        public async Task<PropertyResponse> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
            var property = await _propertiesService.GetPropertyByIdAsync(request.Id);

            _mapper.Map(request, property);

            await _propertiesService.UpdatePropertyAsync(property);

            return _mapper.Map<PropertyResponse>(property);
        }
    }
}
