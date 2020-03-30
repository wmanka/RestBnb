using System.Collections.Generic;

namespace RestBnb.Core.Contracts.V1.Responses
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}