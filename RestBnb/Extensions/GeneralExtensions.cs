using Microsoft.AspNetCore.Http;
using System.Linq;

namespace RestBnb.Core.Extensions
{
    public static class GeneralExtensions
    {
        public static int GetCurrentUserId(this HttpContext httpContext)
        {
            return httpContext.User == null
                ? default
                : int.Parse(httpContext.User.Claims.Single(x => x.Type == "id").Value);
        }
    }
}
