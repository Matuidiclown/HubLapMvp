using HubLap.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HubLap.Data.Interfaces
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetRooms();
        Task AddRoom(Room room);
        // Aquí puedes agregar más métodos a futuro: GetRoomById, UpdateRoom, etc.
    }
}
