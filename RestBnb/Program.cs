using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using RestBnb.API.Extensions;
using System.Threading.Tasks;

namespace RestBnb.API
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var host = await CreateHostBuilder(args).Build().SeedAsync();

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host
                .CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
        }
    }
}