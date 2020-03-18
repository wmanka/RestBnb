using Microsoft.EntityFrameworkCore;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Entities;
using RestBnb.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestBnb.API.Services
{
    public class RolesService : IRolesService
    {
        private readonly DataContext _dataContext;

        public RolesService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> CreateRoleAsync(Role role)
        {
            await _dataContext.Roles.AddAsync(role);
            var created = await _dataContext.SaveChangesAsync();

            return created > 0;
        }

        public async Task<bool> DeleteRoleAsync(int roleId)
        {
            var role = await GetRoleByIdAsync(roleId);

            if (role == null)
                return false;

            _dataContext.Roles.Remove(role);
            var removed = await _dataContext.SaveChangesAsync();

            return removed > 0;
        }

        public async Task<Role> GetRoleByIdAsync(int roleId)
        {
            return await _dataContext.Roles.SingleOrDefaultAsync(x => x.Id == roleId);
        }

        public async Task<Role> GetRoleByNameAsync(string name)
        {
            return await _dataContext.Roles.SingleOrDefaultAsync(x => x.Name == name);
        }

        public async Task<List<Role>> GetRolesAsync()
        {
            return await _dataContext.Roles.ToListAsync();
        }

        public async Task<bool> UpdateRoleAsync(Role roleToUpdate)
        {
            _dataContext.Roles.Update(roleToUpdate);

            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }
    }
}