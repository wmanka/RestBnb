using FluentValidation;
using RestBnb.API.Application.Bookings.Commands;
using System;

namespace RestBnb.API.Validators.Bookings.Commands
{
    public class DeleteBookingCommandValidator : AbstractValidator<DeleteBookingCommand>
    {
        public DeleteBookingCommandValidator(IServiceProvider serviceProvider)
        {
            RuleFor(booking => booking)
                .SetValidator(new DomainValidator())
                .DependentRules(() => RuleFor(booking => booking)
                    .SetValidator(new BusinessValidator(serviceProvider)));
        }

        private class DomainValidator : AbstractValidator<DeleteBookingCommand>
        {
            internal DomainValidator()
            {
                RuleFor(booking => booking.Id)
                    .NotNull()
                    .GreaterThan(0);
            }
        }

        private class BusinessValidator : AbstractValidator<DeleteBookingCommand>
        {
            internal BusinessValidator(IServiceProvider serviceProvider)
            {
                CascadeMode = CascadeMode.Stop;

                RuleFor(booking => booking)
                    .MustExist(serviceProvider)
                    .MustBeOwnedByCurrentUser(serviceProvider)
                    .MustNotBeInProgress(serviceProvider);
            }
        }
    }
}
