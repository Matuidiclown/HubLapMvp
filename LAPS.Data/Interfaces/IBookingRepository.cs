using LAPS.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LAPS.Data.Interfaces
{
    public interface IBookingRepository
    {
        Task<int> Create(BookingHeader h, BookingDetail d);
        Task<IEnumerable<BookingHeader>> GetAll();
        Task<BookingHeader?> GetById(int id);
        Task<IEnumerable<BookingHeader>> GetByUserId(string userId);
        Task<BookingDetail?> GetDetailByBookingId(int bookingId);
        Task Update(BookingHeader h, BookingDetail d);
        Task Delete(int id);
        Task<bool> UpdateStatus(int bookingId, int status);
    }
}