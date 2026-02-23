using HubLap.Business.Interfaces;
using HubLap.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HubLap.Web.Controllers
{
    [Route("api/[controller]")] // Define la ruta como api/Booking
    [ApiController]             // Le avisa a Swagger que esto es una API
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IRoomService _roomService;

        public BookingController(IBookingService bookingService, IRoomService roomService)
        {
            _bookingService = bookingService;
            _roomService = roomService;
        }

        // 1. GET: api/Booking (Listar todas)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookings = await _bookingService.GetAllBookings();

            // Si la lista es nula o no tiene elementos
            if (bookings == null || !bookings.Any())
            {
                return Ok(new { message = "No se encontraron reservas registradas en el sistema." });
            }

            return Ok(bookings);
        }

        // 2. GET: api/Booking/5 (Obtener por ID)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var booking = await _bookingService.GetBookingById(id);

            if (booking == null)
            {
                return NotFound(new { message = $"La reserva con ID {id} no existe o ha sido cancelada." });
            }

            return Ok(booking);
        }

        // 3. POST: api/Booking (Crear)
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookingHeader booking)
        {
            if (booking == null) return BadRequest("Datos inválidos");

            try
            {
                await _bookingService.CreateBooking(booking);
                return Ok(new { message = "Reserva creada exitosamente" });
            }
            catch (ArgumentException ex)
            {
                // Esto envía un JSON claro que Swagger sí puede leer
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                // Para cualquier otro error inesperado
                return StatusCode(500, new { error = "Ocurrió un error interno: " + ex.Message });
            }
        }

        // 4. PUT: api/Booking/5 (Actualizar)
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] BookingHeader booking)
        {
            try
            {
                await _bookingService.UpdateBooking(booking);
                return Ok(new { message = "Reserva actualizada con éxito" });
            }
            catch (ArgumentException ex) { return BadRequest(new { error = ex.Message }); }
        }

        // 5. DELETE: api/Booking/5 (Eliminar)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookingService.DeleteBooking(id);
            return Ok(new { message = "Reserva cancelada correctamente" });
        }
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var bookings = await _bookingService.GetBookingsByUserId(userId);
            if (bookings == null || !bookings.Any())
                return Ok(new { message = "Este usuario no tiene reservas activas." });

            return Ok(bookings);
        }
    }
}