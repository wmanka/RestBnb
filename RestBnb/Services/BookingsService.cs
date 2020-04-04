using Microsoft.EntityFrameworkCore;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Entities;
using RestBnb.Infrastructure;
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

        public async Task<bool> UpdateBookingAsync(Booking bookingToUpdate)
        {
            _dataContext.Bookings.Update(bookingToUpdate);

            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteBookingAsync(int bookingId)
        {
            var booking = await GetBookingByIdAsync(bookingId);

            if (booking == null)
                return false;

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

        private static IQueryable<Booking> AddFiltersOnQuery(GetAllBookingsFilter filter, IQueryable<Booking> bookings)
        {
            //if (filter?.MaxPricePerNight > 0)
            //{
            //    bookings = bookings.Where(x => x.PricePerNight <= filter.MaxPricePerNight);
            //}


            return bookings;
        }
    }
}
