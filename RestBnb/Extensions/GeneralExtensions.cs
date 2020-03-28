using Microsoft.AspNetCore.Http;
using System.Linq;

namespace RestBnb.API.Extensions
{
    public static class GeneralExtensions
    {
        public static int GetCurrentUserId(this HttpContext httpContext)
        {
            if (httpContext.User == null)
            {
                return default;
            }

            return int.Parse(httpContext.User.Claims.Single(x => x.Type == "id").Value);
        }
    }
}
