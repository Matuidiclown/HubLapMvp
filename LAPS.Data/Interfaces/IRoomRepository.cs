using LAPS.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LAPS.Data.Interfaces
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAll();
        Task<IEnumerable<Room>> GetByCategory(int categoryId);
        Task Create(Room room);
        Task Update(Room room);
        Task Delete(int id);
        Task<IEnumerable<dynamic>> GetCategories();
        Task<bool> IsAvailable(int roomId, DateTime start, DateTime end);
    }
}