using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestBnb.API.Options;

namespace RestBnb.API.Extensions
{
    public static class SwaggerConfigurationExtensions
    {
        /// <summary>
        /// Adds middleware for creating documentation using Swagger
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        public static void UseSwagger(this IApplicationBuilder app, IConfiguration configuration)
        {
            var swaggerOptions = GetSwaggerOptions(configuration);

            app.UseSwagger(options => options.RouteTemplate = swaggerOptions.JsonRoute);
            app.UseSwaggerUI(options => options.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description));
        }

        private static SwaggerOptions GetSwaggerOptions(IConfiguration configuration)
        {
            var swaggerOptions = new SwaggerOptions();
            configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

            return swaggerOptions;
        }
    }
}