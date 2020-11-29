using MediatR;
using RestBnb.API.Application.PropertyImages.Responses;
using System.Collections.Generic;

namespace RestBnb.API.Application.PropertyImages.Queries
{
    public class GetAllPropertyImagesQuery : IRequest<IEnumerable<PropertyImageResponse>>
    {
        public int PropertyId { get; }

        public GetAllPropertyImagesQuery(int propertyId)
        {
            PropertyId = propertyId;
        }
    }
}
