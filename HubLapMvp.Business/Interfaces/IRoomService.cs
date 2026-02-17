using HubLap.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HubLap.Business.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAllRooms();
        Task CreateRoom(Room room);
    }
}