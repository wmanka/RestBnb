using Microsoft.AspNetCore.Http;

namespace RestBnb.API.Application.PropertyImages.Requests
{
    public class CreatePropertyImageRangeRequest
    {
        public IFormFile[] Images { get; set; }
    }
}
