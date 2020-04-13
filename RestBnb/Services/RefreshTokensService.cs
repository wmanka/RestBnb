using Microsoft.EntityFrameworkCore;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Entities;
using RestBnb.Infrastructure;
using System.Threading.Tasks;

namespace RestBnb.API.Services
{
    public class RefreshTokensService : IRefreshTokensService
    {
        private readonly DataContext _dataContext;

        public RefreshTokensService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> CreateRefreshTokenAsync(RefreshToken refreshToken)
        {
            await _dataContext.RefreshTokens.AddAsync(refreshToken);
            var created = await _dataContext.SaveChangesAsync();

            return created > 0;
        }

        public async Task<bool> UpdateRefreshTokenAsync(RefreshToken refreshToken)
        {
            _dataContext.RefreshTokens.Update(refreshToken);
            var updated = await _dataContext.SaveChangesAsync();

            return updated > 0;
        }

        public async Task<RefreshToken> GetRefreshTokenByTokenAsync(string token)
        {
            return await _dataContext.RefreshTokens.SingleOrDefaultAsync(x => x.Token == token);
        }
    }
}
