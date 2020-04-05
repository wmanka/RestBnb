using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace RestBnb.API.Services
{
    public class UserResolverService
    {
        private readonly IHttpContextAccessor _context;
        public UserResolverService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public int GetUserId()
        {
            return int.Parse(_context.HttpContext.User.Claims.Single(x => x.Type == "id").Value);
        }

        public ClaimsPrincipal GetUser()
        {
            return _context.HttpContext.User;
        }
    }
}
