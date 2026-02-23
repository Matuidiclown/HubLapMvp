using HubLap.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HubLap.Data.Interfaces
{
    public interface IBookingRepository
    {
        Task<IEnumerable<BookingHeader>> GetAllBookings();
        Task<BookingHeader?> GetBookingById(int id);
        Task<IEnumerable<BookingHeader>> GetBookingsByUserId(int userId);
        Task CreateBooking(BookingHeader booking);
        Task UpdateBooking(BookingHeader booking);
        Task DeleteBooking(int id);
        Task CancelBooking(int id);
    }
}
