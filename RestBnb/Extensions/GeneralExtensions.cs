using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace RestBnb.API.Extensions
{
    public static class GeneralExtensions
    {
        public static int GetCurrentUserId(this HttpContext httpContext)
        {
            var token = httpContext.Request.Headers["Authorization"];
            var decodedToken = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;

            return int.Parse(decodedToken.Claims.First(claim => claim.Type == "id").Value);
        }
    }
}
