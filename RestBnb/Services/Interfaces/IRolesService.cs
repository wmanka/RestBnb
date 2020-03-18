using RestBnb.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestBnb.API.Services.Interfaces
{
    public interface IRolesService
    {
        Task<List<Role>> GetRolesAsync();

        Task<bool> CreateRoleAsync(Role role);

        Task<Role> GetRoleByNameAsync(string name);

        Task<Role> GetRoleByIdAsync(int roleId);

        Task<bool> UpdateRoleAsync(Role roleToUpdate);

        Task<bool> DeleteRoleAsync(int roleId);
    }
}