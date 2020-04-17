using FluentValidation.Validators;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RestBnb.API.Services;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Contracts.V1.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RestBnb.API.Validators.Bookings
{
    public class MustBeOwnedByCurrentUser<T> : AsyncPropertyValidatorBase where T : IRequest<BookingResponse>
    {
        private readonly IServiceProvider _serviceProvider;

        public MustBeOwnedByCurrentUser(IServiceProvider serviceProvider) : base("You do not own this booking or property to be booked.")
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task<bool> IsValidAsync(PropertyValidatorContext context, CancellationToken cancellation)
        {
            var value = (T)context.PropertyValue;

            var bookingId = (int)value.GetType().GetProperty("Id")?.GetValue(value, null);

            using var serviceScope = _serviceProvider.CreateScope();
            var bookingsService = serviceScope.ServiceProvider.GetService<IBookingsService>();
            var propertiesService = serviceScope.ServiceProvider.GetService<IPropertiesService>();
            var userResolver = serviceScope.ServiceProvider.GetService<UserResolverService>();

            var booking = await bookingsService.GetBookingByIdAsync(bookingId);

            var isCurrentUserOwnerOfBooking = await bookingsService.DoesUserOwnBookingAsync(userResolver.GetUserId(), booking.Id);
            var isCurrentUserOwnerOfPropertyToBeBooked = await propertiesService.DoesUserOwnPropertyAsync(userResolver.GetUserId(), booking.PropertyId);

            return isCurrentUserOwnerOfBooking || isCurrentUserOwnerOfPropertyToBeBooked;
        }
    }
}
