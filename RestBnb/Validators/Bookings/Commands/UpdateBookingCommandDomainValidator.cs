using FluentValidation;
using RestBnb.API.Application.Bookings.Commands;
using System;

namespace RestBnb.API.Validators.Bookings.Commands
{

    public class UpdateBookingCommandValidator : AbstractValidator<UpdateBookingCommand>
    {
        public UpdateBookingCommandValidator(IServiceProvider serviceProvider)
        {
            RuleFor(booking => booking)
                .SetValidator(new DomainValidator())
                .DependentRules(() => RuleFor(booking => booking)
                    .SetValidator(new BusinessValidator(serviceProvider)));
        }

        private class DomainValidator : AbstractValidator<UpdateBookingCommand>
        {
            internal DomainValidator()
            {
                RuleFor(booking => booking.Id)
                    .NotNull()
                    .GreaterThan(0);

                //RuleFor(booking => booking.CheckInDate.Date).GreaterThanOrEqualTo(DateTime.UtcNow.Date);
                //RuleFor(booking => booking.CheckOutDate.Date).GreaterThanOrEqualTo(booking => booking.CheckInDate);
            }
        }
        private class BusinessValidator : AbstractValidator<UpdateBookingCommand>
        {
            internal BusinessValidator(IServiceProvider serviceProvider)
            {
                CascadeMode = CascadeMode.Stop;

                RuleFor(booking => booking)
                    .MustLastAtLeastOneDay()
                    .MustExist(serviceProvider)
                    .MustBeAvailable(serviceProvider)
                    //.MustBeOwnedByCurrentUser(serviceProvider)
                    .MustNotBeInProgress(serviceProvider);
            }
        }
    }
}
