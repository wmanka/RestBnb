using FluentValidation;
using RestBnb.API.Application.Users.Requests;

namespace RestBnb.API.Validators.Users
{
    public class UserUpdateRequestValidation : AbstractValidator<UpdateUserRequest>
    {
        public UserUpdateRequestValidation()
        {
            RuleFor(user => user.Email).EmailAddress().NotEmpty();
            //RuleFor(user => user.PhoneNumber).Matches(RegexPatterns.User.PhoneNumber).WithMessage("It is not a valid phone number");
        }
    }
}
