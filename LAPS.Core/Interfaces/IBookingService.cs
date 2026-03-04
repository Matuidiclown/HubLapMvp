using LAPS.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LAPS.Business.Interfaces
{
    public interface IBookingService
    {
        Task<int> CreateBooking(BookingHeader h, BookingDetail d);
        Task<IEnumerable<BookingHeader>> GetAllBookings();
        Task<BookingHeader?> GetBookingById(int id);
        Task<IEnumerable<object>> GetBookingsByUserId(string userId);

        Task<object?> GetBookingWithRoomInfo(int id);
        Task UpdateBooking(BookingHeader h, BookingDetail d);
        Task DeleteBooking(int id);
        Task<bool> CancelBooking(int bookingId);
    }
}