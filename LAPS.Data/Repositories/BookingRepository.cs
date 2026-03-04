using LAPS.Data.Core;
using LAPS.Data.Interfaces;
using LAPS.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAPS.Data.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly IDataAccess _db;
        private const string Conn = "DefaultConnection";

        public BookingRepository(IDataAccess db) => _db = db;

        public async Task<int> Create(BookingHeader header, BookingDetail detail)
        {
            string guestsCsv = (detail.GuestIds != null && detail.GuestIds.Any())
                ? string.Join(",", detail.GuestIds)
                : string.Empty;

            var parameters = new
            {
                UserId = header.UserId,
                RoomId = header.RoomId,
                StartTime = detail.StartTime,
                EndTime = detail.EndTime,
                Subject = detail.MeetingSubject,
                Notes = guestsCsv
            };

            // LoadData captura el SELECT @BookingId del final del SP
            var result = await _db.LoadData<int>("sp_CreateBooking", parameters, "DefaultConnection");

            // Si result tiene algo, devuelve el ID, si no, devuelve un error controlado
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<BookingHeader>> GetAll() =>
            await _db.LoadData<BookingHeader>("sp_GetAllBookings", new { }, Conn);

        public async Task<BookingHeader?> GetById(int id)
        {
            var result = await _db.LoadData<BookingHeader>("sp_GetBookingById", new { BookingId = id }, Conn);
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<BookingHeader>> GetByUserId(string userId)
        {
            return await _db.LoadData<BookingHeader>("sp_GetBookingsByUserId", new { UserId = userId }, "DefaultConnection");
        }

        public async Task<BookingDetail?> GetDetailByBookingId(int bookingId)
        {
            var result = await _db.LoadData<BookingDetail>("sp_GetBookingDetailByBookingId", new { BookingId = bookingId }, "DefaultConnection");
            return result.FirstOrDefault();
        }

        public async Task Update(BookingHeader h, BookingDetail d) =>
            await _db.SaveData("sp_UpdateBooking", new { h.BookingId, d.StartTime, d.EndTime, Subject = d.MeetingSubject, d.Notes }, Conn);

        public async Task Delete(int id) =>
            await _db.SaveData("sp_DeleteBooking", new { BookingId = id }, Conn);

        public async Task<bool> UpdateStatus(int bookingId, int status)
        {
            try
            {
                await _db.SaveData("sp_UpdateBookingStatus", new { BookingId = bookingId, Status = status }, Conn);
                return true;
            }
            catch { return false; }
        }

    }
}