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
    public class MustNotBeInProgress<T> : AsyncPropertyValidatorBase where T : IRequest<BookingResponse>
    {
        private readonly IServiceProvider _serviceProvider;

        public MustNotBeInProgress(IServiceProvider serviceProvider) : base("You cannot modify booking after it has already started.")
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task<bool> IsValidAsync(PropertyValidatorContext context, CancellationToken cancellation)
        {
            var value = (T)context.PropertyValue;

            var bookingId = (int)value.GetType().GetProperty("Id")?.GetValue(value, null);

            using var serviceScope = _serviceProvider.CreateScope();
            var bookingsService = serviceScope.ServiceProvider.GetService<IBookingsService>();

            return !await bookingsService.IsBookingInProgressAsync(bookingId);
        }
    }
}
