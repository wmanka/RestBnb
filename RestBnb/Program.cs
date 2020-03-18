using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Entities;
using RestBnb.Infrastructure;
using System.Threading.Tasks;

namespace RestBnb
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<DataContext>();

                await dbContext.Database.MigrateAsync();

                var roleService = serviceScope.ServiceProvider.GetRequiredService<IRolesService>();

                var adminRole = await roleService.GetRoleByNameAsync("Admin");
                if (adminRole == null)
                {
                    await roleService.CreateRoleAsync(new Role { Name = "Admin" });
                }

                var userRole = await roleService.GetRoleByNameAsync("User");
                if (userRole == null)
                {
                    await roleService.CreateRoleAsync(new Role { Name = "User" });
                }
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}