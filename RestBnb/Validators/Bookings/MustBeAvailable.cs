using FluentValidation.Validators;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Contracts.V1.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RestBnb.API.Validators.Bookings
{
    public class MustBeAvailable<T> : AsyncPropertyValidatorBase where T : IRequest<BookingResponse>
    {
        private readonly IServiceProvider _serviceProvider;

        public MustBeAvailable(IServiceProvider serviceProvider) : base("Property is not available during given date range.")
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task<bool> IsValidAsync(PropertyValidatorContext context, CancellationToken cancellation)
        {
            using var serviceScope = _serviceProvider.CreateScope();
            var bookingsService = serviceScope.ServiceProvider.GetService<IBookingsService>();

            var value = (T)context.PropertyValue;

            var propertyInfo = value.GetType().GetProperty("Id");

            var bookingId = propertyInfo != null ? (int)propertyInfo.GetValue(value, null) : default;

            var checkInDate = (DateTime)value.GetType().GetProperty("CheckInDate")?.GetValue(value, null);
            var checkOutDate = (DateTime)value.GetType().GetProperty("CheckOutDate")?.GetValue(value, null);

            int propertyId;

            if (bookingId != default)
            {
                var booking = await bookingsService.GetBookingByIdAsync(bookingId);
                propertyId = booking.PropertyId;
            }
            else
            {
                propertyId = (int)value.GetType().GetProperty("PropertyId")?.GetValue(value, null);
            }

            return await bookingsService
                .IsPropertyAvailableWithinDateRangeAsync(
                    propertyId,
                    checkInDate,
                    checkOutDate,
                    bookingId);
        }
    }
}
