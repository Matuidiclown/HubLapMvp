using LAPS.Business.Interfaces;
using LAPS.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LAPS.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _service;
        public RoomsController(IRoomService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllRooms());

        [HttpGet("category/{id}")]
        public async Task<IActionResult> GetByCat(int id) => Ok(await _service.GetRoomsByCategory(id));

        [HttpGet("categories")]
        public async Task<IActionResult> GetCats() => Ok(await _service.GetRoomCategories());

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Room room)
        {
            await _service.CreateRoom(room);
            return Ok(new { Message = "Sala creada exitosamente." });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Room room)
        {
            await _service.UpdateRoom(room);
            return Ok(new { Message = "Sala actualizada correctamente." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteRoom(id);
            return Ok(new { Message = "Sala eliminada." });
        }
    }
}