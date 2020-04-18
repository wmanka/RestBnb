using FluentValidation;
using RestBnb.API.Application.Auth.Commands;
using System;

namespace RestBnb.API.Validators.Auth.Commands
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator(IServiceProvider serviceProvider)
        {
            RuleFor(request => request.Token).NotNull().NotEmpty();
            RuleFor(request => request.RefreshToken).NotNull().NotEmpty();

            RuleFor(request => request)
                .TokenMustBeExpired(serviceProvider)
                .TokenMustNotBeInvalidatedOrUsed(serviceProvider);
        }
    }
}
