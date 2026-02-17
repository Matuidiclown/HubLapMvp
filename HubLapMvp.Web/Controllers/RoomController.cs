using HubLap.Business.Interfaces;
using HubLap.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HubLap.Web.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        // GET: Room
        public async Task<IActionResult> Index()
        {
            var rooms = await _roomService.GetAllRooms();
            return View(rooms); // Necesitarás crear la vista Index.cshtml
        }

        // GET: Room/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Room/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Room room)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _roomService.CreateRoom(room);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(room);
        }
    }
}
