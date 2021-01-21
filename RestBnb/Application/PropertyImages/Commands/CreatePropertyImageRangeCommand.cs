using MediatR;
using Microsoft.AspNetCore.Http;
using RestBnb.API.Application.PropertyImages.Responses;

namespace RestBnb.API.Application.PropertyImages.Commands
{
    public class CreatePropertyImageRangeCommand : IRequest<PropertyImageResponse>
    {
        public CreatePropertyImageRangeCommand(int propertyId)
        {
            PropertyId = propertyId;
        }

        public int PropertyId { get; }
        public IFormFile Image { get; set; }
    }
}
