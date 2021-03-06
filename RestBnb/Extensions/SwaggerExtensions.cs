﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using RestBnb.Core.Options;

namespace RestBnb.API.Extensions
{
    public static class SwaggerExtensions
    {
        public static void UseSwagger(this IApplicationBuilder app, IConfiguration configuration)
        {
            var swaggerOptions = GetSwaggerOptions(configuration);

            app.UseSwagger(options => options.RouteTemplate = swaggerOptions.JsonRoute);

            app.UseSwaggerUI(options => options
                .SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description));
        }

        private static SwaggerOptions GetSwaggerOptions(IConfiguration configuration)
        {
            var swaggerOptions = new SwaggerOptions();
            configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

            return swaggerOptions;
        }
    }
}