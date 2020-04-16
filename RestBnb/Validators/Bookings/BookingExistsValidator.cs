using FluentValidation.Validators;
using Microsoft.Extensions.DependencyInjection;
using RestBnb.API.Services.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RestBnb.API.Validators.Bookings
{
    public class MustExist<T> : PropertyValidatorAsyncBase where T : class
    {
        private readonly IServiceProvider _serviceProvider;

        public MustExist(IServiceProvider serviceProvider) : base("Booking does not exist.")
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task<bool> IsValidAsync(PropertyValidatorContext context, CancellationToken cancellation)
        {
            var value = (T)context.PropertyValue;

            var id = (int)value.GetType().GetProperty("Id")?.GetValue(value, null);

            using var serviceScope = _serviceProvider.CreateScope();
            var bookingsService = serviceScope.ServiceProvider.GetService<IBookingsService>();

            var booking = await bookingsService.GetBookingByIdAsync(id);

            return booking != null;
        }
    }
}
