using RestBnb.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestBnb.API.Services
{
    public interface IUserService
    {
        Task<List<User>> GetUsersAsync();

        Task<bool> CreateUserAsync(User user);

        Task<User> GetUserByIdAsync(int userId);

        Task<User> GetUserByEmailAsync(string email);

        Task<bool> UpdateUserAsync(User userToUpdate);

        Task<bool> DeleteUserAsync(int userId);

        Task<bool> CheckPasswordAsync(User user, string password);
    }
}