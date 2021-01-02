using MediatR;
using RestBnb.API.Application.PropertyImages.Commands;
using RestBnb.API.Application.PropertyImages.Responses;
using RestBnb.API.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RestBnb.API.Application.PropertyImages.Handlers
{
    public class DeletePropertyImageHandler : IRequestHandler<DeletePropertyImageCommand, PropertyImageResponse>
    {
        private readonly IPropertyImagesService _propertyImagesService;

        public DeletePropertyImageHandler(IPropertyImagesService propertyImagesService)
        {
            _propertyImagesService = propertyImagesService;
        }

        public async Task<PropertyImageResponse> Handle(DeletePropertyImageCommand request, CancellationToken cancellationToken)
        {
            await _propertyImagesService.DeleteAsync(request.ImageId);

            return null;
        }
    }
}
