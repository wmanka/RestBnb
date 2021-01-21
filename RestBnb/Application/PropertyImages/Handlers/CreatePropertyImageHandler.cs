using AutoMapper;
using MediatR;
using RestBnb.API.Application.PropertyImages.Commands;
using RestBnb.API.Application.PropertyImages.Responses;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace RestBnb.API.Application.PropertyImages.Handlers
{
    public class CreatePropertyImageHandler : IRequestHandler<CreatePropertyImageRangeCommand, PropertyImageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPropertyImagesService _propertyImagesService;

        public CreatePropertyImageHandler(IMapper mapper, IPropertyImagesService propertyImagesService)
        {
            _mapper = mapper;
            _propertyImagesService = propertyImagesService;
        }

        public async Task<PropertyImageResponse> Handle(CreatePropertyImageRangeCommand request, CancellationToken cancellationToken)
        {
            var image = _mapper.Map<PropertyImage>(request);

            await _propertyImagesService.CreateAsync(image);

            return _mapper.Map<PropertyImageResponse>(image);
        }
    }
}
