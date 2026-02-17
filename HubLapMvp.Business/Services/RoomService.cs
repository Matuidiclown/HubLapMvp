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

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<IEnumerable<Room>> GetAllRooms()
        {
            return await _roomRepository.GetRooms();
        }

        public async Task CreateRoom(Room room)
        {
            // Validaciones de Negocio
            if (string.IsNullOrWhiteSpace(room.Name))
                throw new ArgumentException("El nombre de la sala es obligatorio.");

            if (room.Capacity <= 0)
                throw new ArgumentException("La capacidad debe ser mayor a 0.");

            if (string.IsNullOrWhiteSpace(room.Location))
                throw new ArgumentException("La ubicación es obligatoria.");

            await _roomRepository.AddRoom(room);
        }
    }
}