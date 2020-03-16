using Microsoft.EntityFrameworkCore;
using RestBnb.Core.Entities;
using RestBnb.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestBnb.API.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;

        public UserService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            await _dataContext.Users.AddAsync(user);
            var created = await _dataContext.SaveChangesAsync();

            return created > 0;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await GetUserByIdAsync(userId);

            if (user == null)
                return false;

            _dataContext.Users.Remove(user);
            var removed = await _dataContext.SaveChangesAsync();

            return removed > 0;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _dataContext.Users.SingleOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _dataContext.Users.SingleOrDefaultAsync(x => x.Email == email);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _dataContext.Users.ToListAsync();
        }

        public async Task<bool> UpdateUserAsync(User userToUpdate)
        {
            _dataContext.Users.Update(userToUpdate);

            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            var userFromDb = await GetUserByIdAsync(user.Id);

            return true;
        }
    }
}