using RestBnb.Core.Entities;
using System.Threading.Tasks;

namespace RestBnb.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password);

        Task<AuthenticationResult> LoginAsync(string email, string password);

        Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);
    }
}