using Microsoft.EntityFrameworkCore;
using RestBnb.API.Helpers;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Entities;
using RestBnb.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestBnb.API.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;
        private readonly IRolesService rolesService;

        public UserService(
            DataContext dataContext,
            IRolesService rolesService)
        {
            _dataContext = dataContext;
            this.rolesService = rolesService;
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

        public async Task<bool> CheckPasswordAsync(string email, string password)
        {
            var user = await GetUserByEmailAsync(email);

            return PasswordHasherHelper.IsPasswordHashCorrect(password, user.PasswordHash, user.PasswordSalt);
        }

        public async Task<IEnumerable<Role>> GetRolesAsync(User user)
        {
            return await _dataContext
                .UserRoles
                .Where(x => x.UserId == user.Id)
                .Select(x => x.Role)
                .ToListAsync();
        }

        public async Task<bool> AddToRoleAsync(User user, string roleName)
        {
            var roleInDatabase = await rolesService.GetRoleByNameAsync(roleName);

            if (roleInDatabase == null)
            {
                return false;
            }

            var userInDatabase = await GetUserByIdAsync(user.Id);
            if (userInDatabase == null)
                return false;

            var userRoleInDatabase = await _dataContext.UserRoles
                .SingleOrDefaultAsync(x => x.UserId == user.Id && x.Role.Name == roleName);

            if (userRoleInDatabase != null)
                return false;

            await _dataContext.UserRoles.AddAsync(new UserRole { RoleId = roleInDatabase.Id, UserId = user.Id });

            var added = await _dataContext.SaveChangesAsync();

            return added > 0;
        }
    }
}