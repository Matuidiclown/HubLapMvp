using LAPS.Models.Entities;

namespace LAPS.Business.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAllRooms(); // Este es el nombre oficial
        Task<IEnumerable<Room>> GetRoomsByCategory(int categoryId);
        Task CreateRoom(Room room);
        Task UpdateRoom(Room room);
        Task DeleteRoom(int id);
        Task<IEnumerable<dynamic>> GetRoomCategories();
    }
}