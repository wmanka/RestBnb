using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Contracts.V1;
using RestBnb.Core.Entities;
using RestBnb.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace RestBnb.API.Extensions
{
    public static class HostExtensions
    {
        public static async Task<IHost> SeedAsync(this IHost host)
        {
            using var serviceScope = host.Services.CreateScope();

            var dataContext = serviceScope.ServiceProvider.GetRequiredService<DataContext>();

            await dataContext.Database.MigrateAsync();

            await EnsureRolesAreCreatedAsync(serviceScope);

            await EnsureCountriesAreCreatedAsync(dataContext, serviceScope);

            return host;
        }

        private static async Task EnsureCountriesAreCreatedAsync(DataContext dataContext, IServiceScope serviceScope)
        {
            if (!dataContext.Countries.Any())
            {
                var jsonConverterService = serviceScope.ServiceProvider.GetRequiredService<ICountriesConverterService>();

                await jsonConverterService.CreateCountriesWithStatesAndCitiesFromJsonAndAddThemToDatabase();
            }
        }

        private static async Task EnsureRolesAreCreatedAsync(IServiceScope serviceScope)
        {
            var roleService = serviceScope.ServiceProvider.GetRequiredService<IRolesService>();

            var roles = await roleService.GetRolesAsync();

            if (!roles.Any())
            {
                foreach (var role in typeof(ApiRoles).GetFields().Select(x => x.Name).ToList())
                {
                    await roleService.CreateRoleAsync(new Role { Name = role });
                }
            }
        }
    }
}