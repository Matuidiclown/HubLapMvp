using HubLap.Data.Core;
using HubLap.Data.Interfaces;
using HubLap.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HubLap.Data.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly IDataAccess _db;

        public BookingRepository(IDataAccess db)
        {
            _db = db;
        }

        public async Task CreateBooking(BookingHeader booking)
        {
            await _db.SaveData("sp_InsertBooking", new
            {
                booking.UserId,
                booking.RoomID,
                booking.BookingStart,
                booking.BookingEnd,
                booking.Subject
            });
        }

        public async Task<IEnumerable<BookingHeader>> GetAllBookings()
        {
            
            return await _db.LoadData<BookingHeader>("sp_GetAllBookings", new { });
        }
        public async Task<BookingHeader?> GetBookingById(int id)
        {
            var result = await _db.LoadData<BookingHeader>("sp_GetBookingById", new { Id = id });
            return result.FirstOrDefault(); 
        }

        public async Task UpdateBooking(BookingHeader booking)
        {
            await _db.SaveData("sp_UpdateBooking", new
            {
                Id = booking.Id,
                Subject = booking.Subject,
                BookingStart = booking.BookingStart,
                BookingEnd = booking.BookingEnd
            });
        }

        public async Task DeleteBooking(int id)
        {
            await _db.SaveData("sp_DeleteBooking", new { Id = id });
        }
    }
}
