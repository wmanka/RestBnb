using System.Collections.Generic;

namespace RestBnb.API.Middleware
{
    public class ErrorResponse
    {
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
}
