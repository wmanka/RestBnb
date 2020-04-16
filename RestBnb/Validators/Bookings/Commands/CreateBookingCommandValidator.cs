using FluentValidation;
using RestBnb.API.Application.Bookings.Commands;
using System;

namespace RestBnb.API.Validators.Bookings.Commands
{
    public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingCommandValidator(IServiceProvider serviceProvider)
        {
            RuleFor(booking => booking)
                .SetValidator(new DomainValidator())
                .DependentRules(() => RuleFor(booking => booking)
                    .SetValidator(new BusinessValidator(serviceProvider)));
        }

        private class DomainValidator : AbstractValidator<CreateBookingCommand>
        {
            internal DomainValidator()
            {
                RuleFor(booking => booking.PropertyId)
                    .NotNull()
                    .GreaterThan(0);

                RuleFor(booking => booking.CheckInDate.Date)
                    .NotEmpty()
                    .GreaterThanOrEqualTo(DateTime.UtcNow.Date);

                RuleFor(booking => booking.CheckOutDate.Date)
                    .NotEmpty()
                    .GreaterThanOrEqualTo(booking => booking.CheckInDate);
            }
        }
        private class BusinessValidator : AbstractValidator<CreateBookingCommand>
        {
            internal BusinessValidator(IServiceProvider serviceProvider)
            {
                CascadeMode = CascadeMode.StopOnFirstFailure;

                RuleFor(booking => booking)
                    .MustLastAtLeastOneDay()
                    .MustBeAvailable(serviceProvider);
            }
        }
    }
}
