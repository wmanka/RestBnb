using MediatR;
using RestBnb.API.Application.PropertyImages.Responses;

namespace RestBnb.API.Application.PropertyImages.Commands
{
    public class CreatePropertyImageCommand : IRequest<PropertyImageResponse>
    {
        public CreatePropertyImageCommand(int propertyId)
        {
            PropertyId = propertyId;
        }

        public int PropertyId { get; }
        public byte[] Image { get; set; }
    }
}
