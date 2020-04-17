﻿using FluentValidation;
using RestBnb.Core.Constants;
using RestBnb.Core.Contracts.V1.Requests.Auth;

namespace RestBnb.API.Validators.Auth
{
    public class UserRegistrationRequestValidator : AbstractValidator<UserRegistrationRequest>
    {
        public UserRegistrationRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .Matches(RegexPatterns.User.Password)
                .WithMessage("Password must be at least 8 characters long and include at least one number, upper case letter and special character");
        }
    }
}