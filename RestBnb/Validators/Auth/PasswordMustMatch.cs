using FluentValidation.Validators;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RestBnb.API.Application.Auth.Responses;
using RestBnb.API.Services.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RestBnb.API.Validators.Auth
{
    public class PasswordMustMatch<T> : AsyncPropertyValidatorBase where T : IRequest<AuthResponse>
    {
        private readonly IServiceProvider _serviceProvider;

        public PasswordMustMatch(IServiceProvider serviceProvider) : base("Password does not match.")
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task<bool> IsValidAsync(PropertyValidatorContext context, CancellationToken cancellation)
        {
            using var serviceScope = _serviceProvider.CreateScope();
            var usersService = serviceScope.ServiceProvider.GetService<IUsersService>();

            var value = (T)context.PropertyValue;
            var email = (string)value.GetType().GetProperty("Email")?.GetValue(value, null);
            var password = (string)value.GetType().GetProperty("Password")?.GetValue(value, null);

            return await usersService.VerifyPasswordAsync(email, password);
        }

    }
}
