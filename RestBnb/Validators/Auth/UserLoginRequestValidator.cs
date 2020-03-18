using FluentValidation;
using RestBnb.API.Contracts.V1.Requests;

namespace RestBnb.API.Validators.Auth
{
    public class UserLoginRequestValidator : AbstractValidator<UserLoginRequest>
    {
        public UserLoginRequestValidator()
        {
            RuleFor(x => x.Email)
               .NotEmpty()
               .EmailAddress();

            RuleFor(x => x.Password)
                .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\\$%\\^&\\*])(?=.{8,})")
                .WithMessage("Password must be at leats 8 characters long and include at least one number, upper case letter and special character");
        }
    }
}