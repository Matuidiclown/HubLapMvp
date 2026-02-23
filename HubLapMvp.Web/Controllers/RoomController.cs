using HubLap.Business.Interfaces;
using HubLap.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HubLap.Web.Controllers
{
    [ApiController] // Importante para que aparezca en Swagger
    [Route("api/[controller]")]
    public class RoomController : ControllerBase // Cambiado de Controller a ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        // 1. Obtener todas las salas
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rooms = await _roomService.GetAllRooms();
            if (rooms == null || !rooms.Any()) return NotFound("No hay salas registradas.");
            return Ok(rooms);
        }

        // 2. Obtener salas por categoría
        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            // Ahora 'GetRoomsByCategory' ya no saldrá en rojo porque está en la Interface
            var rooms = await _roomService.GetRoomsByCategory(categoryId);

            if (rooms == null || !rooms.Any())
            {
                return Ok(new { message = $"No se encontraron espacios para la categoría {categoryId}." });
            }

            return Ok(rooms);
        }

        // 3. Crear una nueva sala (Escritorio, Cancha, etc.)
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Room room)
        {
            try
            {
                await _roomService.CreateRoom(room);
                return Ok(new { message = "Espacio creado exitosamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // 4. Actualizar sala
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Room room)
        {
            await _roomService.UpdateRoom(room);
            return Ok(new { message = "Espacio actualizado" });
        }

        // 5. Eliminar (Baja lógica)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _roomService.DeleteRoom(id);
            return Ok(new { message = "Espacio desactivado" });
        }
        [HttpGet("categories-list")]
        public async Task<IActionResult> GetCategoriesList()
        {
            var categories = await _roomService.GetRoomCategories();
            return Ok(categories);
        }
    }
}