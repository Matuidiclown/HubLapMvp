using HubLap.Data.Core;
using HubLap.Data.Interfaces;
using HubLap.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HubLap.Data.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly IDataAccess _db;

        public RoomRepository(IDataAccess db) => _db = db;

        // 1. Obtener todas las salas (SOLO UNA VEZ)
        public async Task<IEnumerable<Room>> GetAllRooms()
        {
            
            return await _db.LoadData<Room>("sp_GetAllRooms", new { });
        }

        
        public async Task<IEnumerable<Room>> GetRoomsByCategory(int categoryId)
        {
            
            return await _db.LoadData<Room>("sp_GetRoomsByCategory", new { RoomCategoryId = categoryId });
        }

        public async Task CreateRoom(Room room)
        {
            await _db.SaveData("sp_CreateRoom", new
            {
                room.RoomCategoryId,
                room.Name,
                room.Capacity,
                room.Location,
                room.HasProjector,
                room.HasWhiteboard,
                room.Description
            });
        }

        public async Task UpdateRoom(Room room)
        {
            await _db.SaveData("sp_UpdateRoom", new
            {
                RoomId = room.RoomId,
                RoomCategoryId = room.RoomCategoryId,
                Name = room.Name,
                Capacity = room.Capacity,
                Location = room.Location
            });
        }

        public async Task DeleteRoom(int id)
        {
            // Solo el nombre del SP, sin el código SQL
            string procedureName = "sp_DeleteRoom";

            // Pasamos el parámetro @RoomId que espera el SP
            await _db.SaveData(procedureName, new { RoomId = id });
        }
        public async Task<IEnumerable<dynamic>> GetRoomCategories()
        {
            // Ahora le pasamos el nombre del SP, no el SELECT
            string procedureName = "sp_GetRoomCategories";

            // Al usar dynamic, Dapper creará objetos con propiedades Id y Name automáticamente
            return await _db.LoadData<dynamic>(procedureName, new { });
        }
    }
}