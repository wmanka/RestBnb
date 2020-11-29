using AutoMapper;
using MediatR;
using RestBnb.API.Application.PropertyImages.Queries;
using RestBnb.API.Application.PropertyImages.Responses;
using RestBnb.API.Services.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RestBnb.API.Application.PropertyImages.Handlers
{
    public class GetAllPropertyImagesHandler : IRequestHandler<GetAllPropertyImagesQuery, IEnumerable<PropertyImageResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IPropertyImagesService _propertyImagesService;

        public GetAllPropertyImagesHandler(IMapper mapper, IPropertyImagesService propertyImagesService)
        {
            _mapper = mapper;
            _propertyImagesService = propertyImagesService;
        }

        public async Task<IEnumerable<PropertyImageResponse>> Handle(GetAllPropertyImagesQuery request, CancellationToken cancellationToken)
        {
            var images = await _propertyImagesService.GetAllAsync(request.PropertyId);

            return _mapper.Map<IEnumerable<PropertyImageResponse>>(images);
        }
    }
}
