using Microsoft.EntityFrameworkCore;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Entities;
using RestBnb.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestBnb.API.Services
{
    public class BookingsService : IBookingsService
    {
        private readonly DataContext _dataContext;

        public BookingsService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync(GetAllBookingsFilter filter)
        {
            var bookings = _dataContext.Bookings.AsQueryable();

            bookings = AddFiltersOnQuery(filter, bookings);

            return await bookings.ToListAsync();
        }

        public async Task<bool> CreateBookingAsync(Booking booking)
        {
            await _dataContext.Bookings.AddAsync(booking);
            var created = await _dataContext.SaveChangesAsync();

            return created > 0;
        }

        public async Task<Booking> GetBookingByIdAsync(int bookingId)
        {
            return await _dataContext.Bookings.SingleOrDefaultAsync(x => x.Id == bookingId);
        }

        public async Task<bool> UpdateBookingAsync(Booking booking)
        {
            _dataContext.Bookings.Update(booking);

            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteBookingAsync(int bookingId)
        {
            var booking = await GetBookingByIdAsync(bookingId);

            booking.CancellationDate = DateTime.UtcNow;

            _dataContext.Bookings.Remove(booking);
            var removed = await _dataContext.SaveChangesAsync();

            return removed > 0;
        }

        public async Task<bool> DoesUserOwnBookingAsync(int userId, int bookingId)
        {
            var booking = await _dataContext.Bookings
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == bookingId);

            return booking != null && booking.UserId == userId;
        }

        public async Task<bool> IsPropertyAvailableWithinDateRangeAsync(
            int propertyId,
            DateTime checkInDate,
            DateTime checkOutDate,
            int bookingId = default)
        {
            return !await _dataContext.Bookings
                .AsNoTracking()
                .Where(x =>
                    x.PropertyId == propertyId
                    && x.CheckInDate.Date < checkOutDate.Date
                    && checkInDate.Date < x.CheckOutDate.Date
                    && x.Id != bookingId)
                .AnyAsync();
        }

        public async Task<bool> IsBookingInProgressAsync(int bookingId)
        {
            var booking = await _dataContext.Bookings.AsNoTracking().SingleAsync(x => x.Id == bookingId);

            return DateTime.UtcNow > booking.CheckInDate && DateTime.UtcNow < booking.CheckOutDate;
        }

        private static IQueryable<Booking> AddFiltersOnQuery(GetAllBookingsFilter filter, IQueryable<Booking> bookings)
        {
            if (filter?.PropertyId > 0)
            {
                bookings = bookings.Where(x => x.PropertyId == filter.PropertyId);
            }
            if (filter?.UserId > 0)
            {
                bookings = bookings.Where(x => x.UserId == filter.UserId);
            }
            if (filter?.BookingState != null)
            {
                bookings = bookings.Where(x => x.BookingState == filter.BookingState);
            }
            if (filter?.CheckInDate != null)
            {
                bookings = bookings.Where(x => x.CheckInDate >= filter.CheckInDate);
            }
            if (filter?.CheckOutDate != null)
            {
                bookings = bookings.Where(x => x.CheckOutDate <= filter.CheckOutDate);
            }

            return bookings;
        }
    }
}
