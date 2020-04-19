using FluentValidation;
using RestBnb.API.Application.Auth.Commands;
using RestBnb.Core.Constants;
using System;

namespace RestBnb.API.Validators.Auth.Commands
{
    public class UserRegistrationCommandValidator : AbstractValidator<UserRegistrationCommand>
    {
        public UserRegistrationCommandValidator(IServiceProvider serviceProvider)
        {
            RuleFor(user => user.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(user => user.Password)
                .NotEmpty()
                .Matches(RegexPatterns.User.Password);

            RuleFor(user => user)
                .MustBeUnique(serviceProvider);
        }
    }
}
