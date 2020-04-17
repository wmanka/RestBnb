using FluentValidation;
using RestBnb.API.Application.Users.Commands;
using System;

namespace RestBnb.API.Validators.Users.Commands
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator(IServiceProvider serviceProvider)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(user => user.FirstName).NotEmpty();
            //TODO: phone pattern not working
            //RuleFor(user => user.PhoneNumber).Matches(RegexPatterns.User.PhoneNumber).WithMessage("It is not a valid phone number");

            RuleFor(user => user)
                .MustExist(serviceProvider)
                .MustBeOwnedByCurrentUser(serviceProvider);
        }
    }
}
