using LAPS.Data.Core;
using LAPS.Data.Interfaces;
using LAPS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAPS.Data.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly IDataAccess _db;
        private const string Conn = "DefaultConnection";

        public RoomRepository(IDataAccess db) => _db = db;

        public async Task<IEnumerable<Room>> GetAll() => await _db.LoadData<Room>("sp_GetAllRooms", new { }, Conn);

        public async Task<IEnumerable<Room>> GetByCategory(int categoryId) =>
            await _db.LoadData<Room>("sp_GetRoomsByCategory", new { CategoryId = categoryId }, Conn);

        public async Task Create(Room room)
        {
            var parameters = new
            {
                room.CategoryId,
                room.RoomName,
                room.Location,
                room.Capacity,
                room.HasProjector,
                room.HasWhiteboard,
                room.OperationalStatus,
                room.IsActive
            };

            await _db.SaveData("sp_CreateRoom", parameters, "DefaultConnection");
        }

        public async Task Update(Room room) => await _db.SaveData("sp_UpdateRoom", room, Conn);

        public async Task Delete(int id) => await _db.SaveData("sp_DeleteRoom", new { RoomId = id }, Conn);

        public async Task<IEnumerable<dynamic>> GetCategories() => await _db.LoadData<dynamic>("sp_GetRoomCategories", new { }, Conn);

        public async Task<bool> IsAvailable(int roomId, DateTime start, DateTime end)
        {
            var result = await _db.LoadData<int>("sp_CheckRoomAvailability", new { RoomId = roomId, StartTime = start, EndTime = end }, Conn);
            return result.FirstOrDefault() == 0;
        }
    }
}