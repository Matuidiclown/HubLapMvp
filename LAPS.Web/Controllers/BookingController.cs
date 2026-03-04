using LAPS.Business.Interfaces;
using LAPS.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LAPS.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _service;
        public BookingController(IBookingService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllBookings());

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(string userId) => Ok(await _service.GetBookingsByUserId(userId));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookingRequestDTO request)
        {
            try
            {
                var h = new BookingHeader { UserId = request.UserId, RoomId = request.RoomId };
                var d = new BookingDetail
                {
                    StartTime = request.StartTime,
                    EndTime = request.EndTime,
                    MeetingSubject = request.Subject,
                    GuestIds = request.GuestIds
                };

                var bookingId = await _service.CreateBooking(h, d);

                // En lugar de devolver solo el número, devolvemos un objeto de éxito
                return Ok(new
                {
                    mensaje = "¡Reserva exitosa!",
                    reservaId = bookingId,
                    usuario = request.UserId,
                    salaId = request.RoomId,
                    totalInvitados = request.GuestIds.Count,
                    estado = "Confirmada"
                });
            }
            catch (Exception ex)
            {
                // El mensaje de error que ya teníamos (Aforo superado, etc.)
                return BadRequest(new
                {
                    error = ex.Message
                });
            }
        }

        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetFullBookingDetails(int id)
        {
            var details = await _service.GetBookingWithRoomInfo(id);
            return details != null ? Ok(details) : NotFound(new { message = "Reserva no encontrada" });
        }
    }

    // DTOs ÚNICOS (Definidos una sola vez al final del namespace)
    public class BookingRequestDTO
    {
        public string UserId { get; set; } = string.Empty;
        public int RoomId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Subject { get; set; } = string.Empty;
        public List<string> GuestIds { get; set; } = new List<string>();
    }

    public class BookingUpdateDTO : BookingRequestDTO
    {
        public int BookingId { get; set; }
    }
}
