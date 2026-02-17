using HubLap.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HubLap.Data.Interfaces
{
    public interface IBookingRepository
    {
        Task CreateBooking(BookingHeader booking);
        Task<IEnumerable<BookingHeader>> GetAllBookings();
        Task<BookingHeader?> GetBookingById(int id);
        Task UpdateBooking(BookingHeader booking);
        Task DeleteBooking(int id);
    }
}
