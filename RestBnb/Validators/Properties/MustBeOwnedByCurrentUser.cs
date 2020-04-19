using FluentValidation.Validators;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RestBnb.API.Application.Properties.Responses;
using RestBnb.API.Services;
using RestBnb.API.Services.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RestBnb.API.Validators.Properties
{
    public class MustBeOwnedByCurrentUser<T> : AsyncPropertyValidatorBase where T : IRequest<PropertyResponse>
    {
        private readonly IServiceProvider _serviceProvider;

        public MustBeOwnedByCurrentUser(IServiceProvider serviceProvider) : base("Property does not exist.")
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task<bool> IsValidAsync(PropertyValidatorContext context, CancellationToken cancellation)
        {
            var value = (T)context.PropertyValue;

            var propertyId = (int)value.GetType().GetProperty("Id")?.GetValue(value, null);

            using var serviceScope = _serviceProvider.CreateScope();
            var propertiesService = serviceScope.ServiceProvider.GetService<IPropertiesService>();
            var userResolver = serviceScope.ServiceProvider.GetService<UserResolverService>();

            return await propertiesService.DoesUserOwnPropertyAsync(userResolver.GetUserId(), propertyId);
        }
    }
}
