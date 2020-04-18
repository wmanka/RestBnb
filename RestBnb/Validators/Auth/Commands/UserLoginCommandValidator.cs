using FluentValidation;
using RestBnb.API.Application.Auth.Commands;
using RestBnb.Core.Constants;
using System;

namespace RestBnb.API.Validators.Auth.Commands
{
    public class UserLoginCommandValidator : AbstractValidator<UserLoginCommand>
    {
        public UserLoginCommandValidator(IServiceProvider serviceProvider)
        {
            RuleFor(user => user.Email)
                .EmailAddress()
                .NotEmpty();
            RuleFor(user => user.Password)
                .NotEmpty()
                .Matches(RegexPatterns.User.Password);

            RuleFor(user => user)
                .MustExist(serviceProvider)
                .MustProvideMatchingPassword(serviceProvider);
        }
    }
}
