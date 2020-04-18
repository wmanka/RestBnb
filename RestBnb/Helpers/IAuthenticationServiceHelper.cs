using RestBnb.Core.Entities;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestBnb.API.Helpers
{
    public interface IAuthenticationServiceHelper
    {
        public Task<AuthResponse> GetAuthenticationResultAsync(User user);
        public ClaimsPrincipal GetPrincipalFromToken(string token);
    }
}
