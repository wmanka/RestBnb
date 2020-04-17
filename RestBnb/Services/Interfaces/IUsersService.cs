using RestBnb.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestBnb.API.Services.Interfaces
{
    public interface IUsersService
    {
        Task<IEnumerable<User>> GetUsersAsync();

        Task<bool> CreateUserAsync(User user);

        Task<User> GetUserByIdAsync(int userId);

        Task<User> GetUserByEmailAsync(string email);

        Task<bool> UpdateUserAsync(User userToUpdate);

        Task<bool> DeleteUserAsync(int userId);

        Task<bool> VerifyPasswordAsync(string user, string password);

        Task<bool> AddToRoleAsync(User user, string roleName);

        Task<IEnumerable<Role>> GetRolesAsync(User user);

        bool IsCurrentlyLoggedIn(int userId);
    }
}