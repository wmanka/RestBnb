using AutoMapper;
using MediatR;
using RestBnb.API.Application.PropertyImages.Commands;
using RestBnb.API.Application.PropertyImages.Responses;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Entities;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace RestBnb.API.Application.PropertyImages.Handlers
{
    public class CreatePropertyImageRangeHandler : IRequestHandler<CreatePropertyImageRangeCommand, List<PropertyImageResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IPropertyImagesService _propertyImagesService;

        public CreatePropertyImageRangeHandler(IMapper mapper, IPropertyImagesService propertyImagesService)
        {
            _mapper = mapper;
            _propertyImagesService = propertyImagesService;
        }


        async Task<List<PropertyImageResponse>> IRequestHandler<CreatePropertyImageRangeCommand, List<PropertyImageResponse>>.Handle(
            CreatePropertyImageRangeCommand request,
            CancellationToken cancellationToken)
        {
            var images = new List<PropertyImage>();

            foreach (var file in request.Images)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                using var memoryStream = new MemoryStream();

                await file.CopyToAsync(memoryStream);

                var propertyImage = new PropertyImage()
                {
                    Image = memoryStream.ToArray(),
                    PropertyId = request.PropertyId
                };

                images.Add(propertyImage);

                await _propertyImagesService.CreateAsync(propertyImage);
            }

            return _mapper.Map<List<PropertyImage>, List<PropertyImageResponse>>(images);
        }
    }
}
