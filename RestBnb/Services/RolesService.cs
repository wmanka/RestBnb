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

        public async Task<Role> GetRoleByNameAsync(string name)
        {
            return await _dataContext.Roles.SingleOrDefaultAsync(x => x.Name == name);
        }

        public async Task<List<Role>> GetRolesAsync()
        {
            return await _dataContext.Roles.ToListAsync();
        }
    }
}