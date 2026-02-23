using HubLap.Models.Entities;

namespace HubLap.Business.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAllRooms();
        // AGREGA ESTA LÍNEA:
        Task<IEnumerable<Room>> GetRoomsByCategory(int categoryId);
        Task CreateRoom(Room room);
        Task UpdateRoom(Room room);
        Task DeleteRoom(int id);
        Task<IEnumerable<dynamic>> GetRoomCategories();
    }
}