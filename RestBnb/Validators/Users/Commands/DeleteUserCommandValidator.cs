using FluentValidation;
using RestBnb.API.Application.Users.Commands;
using System;

namespace RestBnb.API.Validators.Users.Commands
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator(IServiceProvider serviceProvider)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(user => user.Id).NotEmpty().GreaterThan(0);

            RuleFor(user => user)
                .MustExist(serviceProvider)
                .MustBeOwnedByCurrentUser(serviceProvider);
        }
    }
}
