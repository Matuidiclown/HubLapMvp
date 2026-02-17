using HubLap.Data.Core;
using HubLap.Data.Interfaces;
using HubLap.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HubLap.Data.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly IDataAccess _db;

        public RoomRepository(IDataAccess db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Room>> GetRooms()
        {
            // Usamos "new { }" porque el SP no pide parámetros
            return await _db.LoadData<Room>("sp_GetAllRooms", new { });
        }

        public async Task AddRoom(Room room)
        {
            await _db.SaveData("sp_InsertRoom", new
            {
                room.RoomTypeId,
                room.Name,
                room.Capacity,
                room.Location,
                room.HasProjector,
                room.HasWhiteboard,
                room.Description
            });
        }
    }
}