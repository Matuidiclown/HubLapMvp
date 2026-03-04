using LAPS.Business.Interfaces;
using LAPS.Data.Interfaces;
using LAPS.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAPS.Business.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _repo;
        public RoomService(IRoomRepository repo) => _repo = repo;

        public async Task<IEnumerable<Room>> GetAllRooms()
        {
            var rooms = await _repo.GetAll();
            return rooms.Any() ? rooms : new List<Room> { new Room { RoomName = "SISTEMA: No hay salas registradas. Por favor, cree una sala primero." } };
        }

        public async Task<IEnumerable<Room>> GetRoomsByCategory(int categoryId)
        {
            var rooms = await _repo.GetByCategory(categoryId);
            return rooms.Any() ? rooms : new List<Room> { new Room { RoomName = $"SISTEMA: No hay salas en la categoría {categoryId}." } };
        }

        public async Task<IEnumerable<dynamic>> GetRoomCategories()
        {
            var cats = await _repo.GetCategories();
            return cats.Any() ? cats : new List<dynamic> { new { Message = "SISTEMA: No hay categorías definidas en la base de datos." } };
        }

        public async Task CreateRoom(Room room)
        {
            
            switch (room.CategoryId)
            {
                case 1: // Ejemplo: Sala Alpha / Directorio
                    room.RoomName = "Sala Alpha";
                    room.Capacity = 10;
                    room.Location = "Piso 1 - Ala Norte";
                    room.HasProjector = true;
                    room.HasWhiteboard = true;
                    break;
                case 2: // Ejemplo: Auditorio
                    room.RoomName = "Auditorio Principal";
                    room.Capacity = 100;
                    room.Location = "Piso 1 - Central";
                    room.HasProjector = true;
                    room.HasWhiteboard = false;
                    break;
                case 3: // Ejemplo: Sala de Innovación
                    room.RoomName = "Sala de Innovación LAPS";
                    room.Capacity = 12;
                    room.Location = "Piso 2, Oficina 204";
                    room.HasProjector = true;
                    room.HasWhiteboard = true;
                    break;
                case 4: // Ejemplo: Sala de Deportiva
                    room.RoomName = "Cancha de fulvo LAPS";
                    room.Capacity = 25;
                    room.Location = "Espalda de outlet";
                    room.HasProjector = false;
                    room.HasWhiteboard = false;
                    break;
                default:
                    
                    room.RoomName = "Sala Genérica";
                    break;
            }

            room.IsActive = true;
            room.OperationalStatus = 1;

            await _repo.Create(room);
        }
        public async Task UpdateRoom(Room room) => await _repo.Update(room);
        public async Task DeleteRoom(int id) => await _repo.Delete(id);
    }
}