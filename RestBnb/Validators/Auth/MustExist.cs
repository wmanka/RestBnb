using FluentValidation.Validators;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RestBnb.API.Validators.Auth
{
    public class MustExist<T> : AsyncPropertyValidatorBase where T : IRequest<AuthResponse>
    {
        private readonly IServiceProvider _serviceProvider;

        public MustExist(IServiceProvider serviceProvider) : base("User does not exist.")
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task<bool> IsValidAsync(PropertyValidatorContext context, CancellationToken cancellation)
        {
            using var serviceScope = _serviceProvider.CreateScope();
            var usersService = serviceScope.ServiceProvider.GetService<IUsersService>();

            var value = (T)context.PropertyValue;
            var email = (string)value.GetType().GetProperty("Email")?.GetValue(value, null);

            var user = await usersService.GetUserByEmailAsync(email);

            return user != null;
        }
    }
}
