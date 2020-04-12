using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestBnb.Infrastructure;

namespace RestBnb.API.Installers
{
    public class HealthChecksInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddHealthChecks()
                .AddDbContextCheck<DataContext>();
        }
    }
}
