using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_2025.Services;

namespace WebAPI_2025.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class RoomController : ControllerBase
    {
        private readonly IRoomService _Service;
        public RoomController(IRoomService roomService)
        {
            _Service = roomService;
        }
        [HttpGet("guesthouse/{GID}")]
        public async Task<IActionResult> GetRoomsByGuestHouseID(int GID)
        {
            var rooms = await _Service.GetRoomByGuesthouseID(GID);
            return Ok(new { count = rooms.Count, rooms });
        }
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableRooms(int guestHouseId, DateTime checkin, DateTime checkout)
        {
            var rooms = await _Service.GetAvailableRooms(guestHouseId, checkin, checkout);
            return Ok(rooms);
        }
    }
}
