using FluentValidation;
using RestBnb.Core.Contracts.V1.Requests.Bookings;
using System;

namespace RestBnb.API.Validators.Bookings
{
    public class BookingUpdateRequestValidator : AbstractValidator<BookingUpdateRequest>
    {
        public BookingUpdateRequestValidator()
        {
            RuleFor(booking => booking.CheckInDate.Date).GreaterThanOrEqualTo(DateTime.UtcNow.Date);
            RuleFor(booking => booking.CheckOutDate.Date).GreaterThanOrEqualTo(booking => booking.CheckInDate);
            RuleFor(booking => booking).Must(LastAtLeastOneDay);
        }

        private static bool LastAtLeastOneDay(BookingUpdateRequest booking)
        {
            return (booking.CheckOutDate - booking.CheckInDate).Days > 1;
        }
    }
}
