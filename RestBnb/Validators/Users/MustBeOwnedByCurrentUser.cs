using FluentValidation.Validators;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RestBnb.API.Application.Users.Responses;
using RestBnb.API.Services.Interfaces;
using System;

namespace RestBnb.API.Validators.Users
{
    public class MustBeOwnedByCurrentUser<T> : PropertyValidator where T : IRequest<UserResponse>
    {
        private readonly IServiceProvider _serviceProvider;

        public MustBeOwnedByCurrentUser(IServiceProvider serviceProvider) : base("You cannot change information about other users.")
        {
            _serviceProvider = serviceProvider;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var value = (T)context.PropertyValue;

            var userId = (int)value.GetType().GetProperty("Id")?.GetValue(value, null);

            using var serviceScope = _serviceProvider.CreateScope();
            var usersService = serviceScope.ServiceProvider.GetService<IUsersService>();

            return usersService.IsCurrentlyLoggedIn(userId);
        }
    }
}
