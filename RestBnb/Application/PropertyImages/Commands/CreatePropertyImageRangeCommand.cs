using MediatR;
using Microsoft.AspNetCore.Http;
using RestBnb.API.Application.PropertyImages.Responses;
using System.Collections.Generic;

namespace RestBnb.API.Application.PropertyImages.Commands
{
    public class CreatePropertyImageRangeCommand : IRequest<List<PropertyImageResponse>>
    {
        public CreatePropertyImageRangeCommand(int propertyId, IFormFile[] images)
        {
            PropertyId = propertyId;
            Images = images;
        }

        public int PropertyId { get; }
        public IFormFile[] Images { get; set; }
    }
}
