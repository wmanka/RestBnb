using FluentValidation;
using RestBnb.Core.Contracts.V1.Requests.Users;

namespace RestBnb.API.Validators.Users
{
    public class UserUpdateRequestValidation : AbstractValidator<UserUpdateRequest>
    {
        public UserUpdateRequestValidation()
        {
            RuleFor(user => user.Email).EmailAddress().NotEmpty();
            RuleFor(user => user.PhoneNumber).Matches(RegexPatterns.User.PhoneNumber).WithMessage("It is not a valid phone number");
        }
    }
}
