using MediatR;
using RestBnb.API.Application.PropertyImages.Responses;

namespace RestBnb.API.Application.PropertyImages.Commands
{
    public class DeletePropertyImageCommand : IRequest<PropertyImageResponse>
    {
        public int PropertyId { get; }
        public int ImageId { get; }

        public DeletePropertyImageCommand(int propertyId, int imageId)
        {
            PropertyId = propertyId;
            ImageId = imageId;
        }
    }
}
