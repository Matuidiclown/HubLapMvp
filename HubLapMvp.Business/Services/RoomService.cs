using HubLap.Business.Interfaces;
using HubLap.Data.Interfaces;
using HubLap.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HubLap.Business.Services
{
    public class RoomService : IRoomService
    {
        
        private readonly IRoomRepository _roomRepository;
        public async Task<IEnumerable<Room>> GetRoomsByCategory(int categoryId)
        {
            // Conectamos el servicio con el repositorio que ya arreglamos
            return await _roomRepository.GetRoomsByCategory(categoryId);
        }

        public RoomService(IRoomRepository roomRepository) => _roomRepository = roomRepository;

        public async Task<IEnumerable<Room>> GetAllRooms() => await _roomRepository.GetAllRooms();

        public async Task CreateRoom(Room room) => await _roomRepository.CreateRoom(room);

        public async Task UpdateRoom(Room room) => await _roomRepository.UpdateRoom(room);

        public async Task DeleteRoom(int id) => await _roomRepository.DeleteRoom(id);
        public async Task<IEnumerable<dynamic>> GetRoomCategories()
        {
            return await _roomRepository.GetRoomCategories();
        }
    }
}