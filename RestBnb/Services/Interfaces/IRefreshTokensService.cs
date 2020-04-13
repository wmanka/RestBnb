using RestBnb.Core.Entities;
using System.Threading.Tasks;

namespace RestBnb.API.Services.Interfaces
{
    public interface IRefreshTokensService
    {
        public Task<bool> CreateRefreshTokenAsync(RefreshToken refreshToken);
        Task<bool> UpdateRefreshTokenAsync(RefreshToken refreshToken);
        Task<RefreshToken> GetRefreshTokenByTokenAsync(string token);
    }
}
