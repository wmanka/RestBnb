using RestBnb.API.Application.Auth.Responses;
using System.Threading.Tasks;

namespace RestBnb.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(string email, string password);

        Task<AuthResponse> LoginAsync(string email, string password);

        Task<AuthResponse> RefreshTokenAsync(string token, string refreshToken);
    }
}