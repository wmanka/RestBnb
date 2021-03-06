﻿using FluentValidation;
using RestBnb.API.Application.Users.Commands;
using RestBnb.Core.Constants;
using System;

namespace RestBnb.API.Validators.Users.Commands
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator(IServiceProvider serviceProvider)
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(user => user.FirstName).NotEmpty();
            RuleFor(user => user.PhoneNumber).Matches(RegexPatterns.User.PhoneNumber).WithMessage("It is not a valid phone number");

            RuleFor(user => user)
                .MustExist(serviceProvider)
                .MustBeOwnedByCurrentUser(serviceProvider);
        }
    }
}
