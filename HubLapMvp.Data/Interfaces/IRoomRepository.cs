using HubLap.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HubLap.Data.Interfaces
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllRooms();
        Task<IEnumerable<Room>> GetRoomsByCategory(int categoryId);
        Task<IEnumerable<dynamic>> GetRoomCategories();
        Task CreateRoom(Room room);
        Task UpdateRoom(Room room);
        Task DeleteRoom(int id);
    }
}
