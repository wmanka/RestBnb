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
    }
}