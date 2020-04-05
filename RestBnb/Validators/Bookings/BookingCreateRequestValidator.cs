using FluentValidation;
using RestBnb.Core.Contracts.V1.Requests;
using System;

namespace RestBnb.API.Validators.Bookings
{
    public class BookingCreateRequestValidator : AbstractValidator<BookingCreateRequest>
    {
        public BookingCreateRequestValidator()
        {
            RuleFor(booking => booking).Must(LastAtLeastOneDay);
            RuleFor(booking => booking.CheckInDate.Date).NotEmpty().GreaterThanOrEqualTo(DateTime.UtcNow.Date);
            RuleFor(booking => booking.CheckOutDate.Date).NotEmpty().GreaterThanOrEqualTo(booking => booking.CheckInDate);
        }

        private static bool LastAtLeastOneDay(BookingCreateRequest booking)
        {
            return (booking.CheckOutDate - booking.CheckInDate).Days > 1;
        }
    }
}
