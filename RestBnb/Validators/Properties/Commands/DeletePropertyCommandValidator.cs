using FluentValidation;
using RestBnb.API.Application.Properties.Commands;
using System;

namespace RestBnb.API.Validators.Properties.Commands
{
    public class DeletePropertyCommandValidator : AbstractValidator<DeletePropertyCommand>
    {
        public DeletePropertyCommandValidator(IServiceProvider serviceProvider)
        {
            RuleFor(booking => booking)
                .SetValidator(new DomainValidator())
                .DependentRules(() => RuleFor(booking => booking)
                    .SetValidator(new BusinessValidator(serviceProvider)));
        }

        private class DomainValidator : AbstractValidator<DeletePropertyCommand>
        {
            internal DomainValidator()
            {
                RuleFor(booking => booking.Id)
                    .NotNull()
                    .GreaterThan(0);
            }
        }

        private class BusinessValidator : AbstractValidator<DeletePropertyCommand>
        {
            internal BusinessValidator(IServiceProvider serviceProvider)
            {
                CascadeMode = CascadeMode.Stop;

                RuleFor(booking => booking)
                    .MustExist(serviceProvider)
                    .MustBeOwnedByCurrentUser(serviceProvider);
            }
        }
    }
}
