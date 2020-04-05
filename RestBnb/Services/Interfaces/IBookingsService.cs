using RestBnb.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestBnb.API.Services.Interfaces
{
    public interface IBookingsService
    {
        Task<IEnumerable<Booking>> GetAllBookingsAsync(GetAllBookingsFilter filter = null);

        Task<bool> CreateBookingAsync(Booking booking);

        Task<Booking> GetBookingByIdAsync(int bookingId);

        Task<bool> UpdateBookingAsync(Booking booking);

        Task<bool> DeleteBookingAsync(int bookingId);
    }
}
