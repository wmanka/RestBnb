using FluentValidation.Validators;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RestBnb.API.Application.Users.Responses;
using RestBnb.API.Services.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RestBnb.API.Validators.Users
{
    public class MustExist<T> : AsyncPropertyValidatorBase where T : IRequest<UserResponse>
    {
        private readonly IServiceProvider _serviceProvider;

        public MustExist(IServiceProvider serviceProvider) : base("User does not exist.")
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task<bool> IsValidAsync(PropertyValidatorContext context, CancellationToken cancellation)
        {
            var value = (T)context.PropertyValue;

            var userId = (int)value.GetType().GetProperty("Id")?.GetValue(value, null);

            using var serviceScope = _serviceProvider.CreateScope();
            var usersService = serviceScope.ServiceProvider.GetService<IUsersService>();

            var user = await usersService.GetUserByIdAsync(userId);

            return user != null;
        }
    }
}
