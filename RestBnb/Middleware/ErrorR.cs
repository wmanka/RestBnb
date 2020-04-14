using System.Collections.Generic;

namespace RestBnb.API.Middleware
{
    public class ErrorR
    {
        public List<ErrorM> Errors { get; set; } = new List<ErrorM>();
    }
}
