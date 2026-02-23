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
            // Enviamos solo los datos necesarios al SP
            await _db.SaveData("sp_CreateBooking", new
            {
                UserId = booking.UserId,
                AssignedRoomId = booking.AssignedRoomId,
                Subject = booking.Subject,
                BookingStart = booking.BookingStart,
                BookingEnd = booking.BookingEnd
            });
        }

        public async Task<IEnumerable<BookingHeader>> GetAllBookings()
        {
            // El nombre debe coincidir EXACTAMENTE con el del SQL
            string procedureName = "sp_GetAllBookings";

            // Al pasarle procedureName, LoadData ya no recibirá un string vacío o un SELECT
            var results = await _db.LoadData<BookingHeader>(procedureName, new { });

            return results;
        }
        public async Task<BookingHeader?> GetBookingById(int id)
        {
            // Forzamos el nombre del SP para evitar el error de "procedure ''"
            string procedureName = "sp_GetBookingById";
            var results = await _db.LoadData<BookingHeader>(procedureName, new { Id = id });
            return results.FirstOrDefault();
        }

        public async Task UpdateBooking(BookingHeader booking)
        {
            await _db.SaveData("sp_UpdateBooking", new
            {
                Id = booking.BookingId,
                Subject = booking.Subject,
                BookingStart = booking.BookingStart,
                BookingEnd = booking.BookingEnd
            });
        }

        public async Task DeleteBooking(int id)
        {
            await _db.SaveData("sp_DeleteRoom", new { RoomId = id });
        }
        public async Task<IEnumerable<BookingHeader>> GetBookingsByUserId(int userId)
        {
            // Forzamos el nombre del procedimiento para que no llegue vacío ''
            string procedureName = "sp_GetBookingsByUserId";

            // El objeto anónimo { UserId = userId } debe coincidir con el @UserId del SQL
            return await _db.LoadData<BookingHeader>(procedureName, new { UserId = userId });
        }
        public async Task CancelBooking(int id)
        {
            // Usamos el SP de cancelación que creamos antes
            await _db.SaveData("sp_CancelBooking", new { BookingId = id });
        }
    }
}
