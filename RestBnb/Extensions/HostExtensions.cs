using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestBnb.API.Contracts.V1;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Entities;
using RestBnb.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace RestBnb.API.Extensions
{
    public static class HostExtensions
    {
        /// <summary>
        /// Applies any pending migrations to the database
        /// </summary>
        /// <param name="host"></param>
        public static async Task ApplyDatabaseMigrationsAsync(this IHost host)
        {
            using var serviceScope = host.Services.CreateScope();

            var dataContext = serviceScope
                            .ServiceProvider
                            .GetRequiredService<DataContext>();

            await dataContext.Database.MigrateAsync();
        }

        /// <summary>
        /// Ensures user roles are created in the database
        /// </summary>
        /// <param name="host"></param>
        public static async Task EnsureRolesAreCreatedAsync(this IHost host)
        {
            using var serviceScope = host.Services.CreateScope();

            var roleService = serviceScope.ServiceProvider.GetRequiredService<IRolesService>();

            var roles = await roleService.GetRolesAsync();
            if (!roles.Any())
            {
                foreach (var role in typeof(ApiRoles).GetFields().Select(x => x.Name).ToList())
                {
                    await roleService.CreateRoleAsync(new Role {Name = role});
                }
            }
        }
    }
}