using FluentValidation.Validators;
using MediatR;
using RestBnb.Core.Contracts.V1.Responses;
using System;

namespace RestBnb.API.Validators.Bookings
{
    public class MustLastAtLeastOneDay<T> : PropertyValidator where T : IRequest<BookingResponse>
    {
        public MustLastAtLeastOneDay() : base("Booking must last at least one day.") { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var value = (T)context.PropertyValue;

            var checkIn = (DateTime)value.GetType().GetProperty("CheckInDate")?.GetValue(value, null);
            var checkOut = (DateTime)value.GetType().GetProperty("CheckOutDate")?.GetValue(value, null);

            return (checkOut - checkIn).Days >= 1;
        }
    }
}
