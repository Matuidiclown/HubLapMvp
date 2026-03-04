using LAPS.Business.Interfaces;
using LAPS.Data.Interfaces;
using LAPS.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAPS.Business.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _repo;
        private readonly IRoomRepository _roomRepo;

        public BookingService(IBookingRepository repo, IRoomRepository roomRepo)
        {
            _repo = repo;
            _roomRepo = roomRepo;
        }

        // 1. Obtener todas las reservas con info de sala
        public async Task<IEnumerable<BookingHeader>> GetAllBookings()
        {
            var bookings = await _repo.GetAll();
            return bookings.Any() ? bookings : new List<BookingHeader> { new BookingHeader { UserId = "SISTEMA: No existen reservas." } };
        }

        // 2. EL MÉTODO DEL CONFLICTO: Unificado para traer info de la sala
        public async Task<IEnumerable<object>> GetBookingsByUserId(string userId)
        {
            var bookings = await _repo.GetByUserId(userId);
            if (bookings == null || !bookings.Any())
            {
                // No dejamos respuestas vacías: enviamos texto informativo
                return new List<object> { new { Message = $"El usuario {userId} no tiene reservas registradas." } };
            }

            var allRooms = await _roomRepo.GetAll();

            // Obtener los detalles de cada reserva
            var result = new List<object>();
            foreach (var b in bookings)
            {
                // Obtener los detalles de esta reserva
                var detail = await _repo.GetDetailByBookingId(b.BookingId);
                
                // Si no hay detalle, usar CreatedAt como fallback
                var startTime = detail?.StartTime ?? b.CreatedAt;
                var endTime = detail?.EndTime ?? b.CreatedAt;
                var subject = detail?.MeetingSubject ?? "Sin asunto";
                var notes = detail?.Notes ?? "";
                
                result.Add(new {
                    ReservaId = b.BookingId,
                    Usuario = b.UserId,
                    Fecha = startTime,  // Ahora devuelve la hora de INICIO de la reunión
                    startDateTime = startTime,
                    startTime = startTime,
                    endDateTime = endTime,
                    endTime = endTime,
                    Asunto = subject,
                    subject = subject,
                    notes = notes,
                    Sala = allRooms.FirstOrDefault(r => r.RoomId == b.RoomId)?.RoomName ?? "Sala no encontrada",
                    Ubicacion = allRooms.FirstOrDefault(r => r.RoomId == b.RoomId)?.Location ?? "N/A",
                    Capacidad = allRooms.FirstOrDefault(r => r.RoomId == b.RoomId)?.Capacity ?? 0
                });
            }
            
            return result;
        }

        // 3. Detalle único (Herencia total)
        public async Task<object?> GetBookingWithRoomInfo(int id)
        {
            var booking = await _repo.GetById(id);
            if (booking == null) return new { Message = "SISTEMA: La reserva no existe." };

            var rooms = await _roomRepo.GetAll();
            var room = rooms.FirstOrDefault(r => r.RoomId == booking.RoomId);

            return new
            {
                Reserva = booking,
                InfoSala = room ?? (object)new { Alerta = "La sala asociada ya no existe." }
            };
        }

        // 4. Métodos CRUD básicos
        public async Task<int> CreateBooking(BookingHeader h, BookingDetail d)
        {
            var allRooms = await _roomRepo.GetAll();
            var room = allRooms.FirstOrDefault(r => r.RoomId == h.RoomId);

            if (room == null)
                throw new Exception("SISTEMA: La sala no existe.");

            // INTERPRETACIÓN: Contamos cuántos códigos mandó el usuario en la lista
            int totalInvitados = d.GuestIds.Count;

            if (totalInvitados > room.Capacity)
            {
                throw new Exception($"SISTEMA: Aforo superado. La sala '{room.RoomName}' solo permite {room.Capacity} personas. Has ingresado {totalInvitados} invitados.");
            }

            return await _repo.Create(h, d);
        }

        public async Task<BookingHeader?> GetBookingById(int id) => await _repo.GetById(id);
        public async Task UpdateBooking(BookingHeader h, BookingDetail d) => await _repo.Update(h, d);
        public async Task DeleteBooking(int id) => await _repo.Delete(id);
        public async Task<bool> CancelBooking(int bookingId) => await _repo.UpdateStatus(bookingId, 0);
    }

}