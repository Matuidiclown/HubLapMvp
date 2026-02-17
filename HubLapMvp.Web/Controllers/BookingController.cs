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
            try
            {
                var bookings = await _bookingService.GetAllBookings();
                if (bookings == null) return NotFound(new { message = "Reserva no encontrada" });
                return Ok(bookings);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // 2. GET: api/Booking/5 (Obtener por ID)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var booking = await _bookingService.GetBookingById(id);
                if (booking == null) return NotFound(new { message = "Reserva no encontrada" });
                return Ok(booking);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
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
    }
}