using Microsoft.AspNetCore.Http;
using System.Linq;

namespace RestBnb.API.Extensions
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
