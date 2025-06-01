using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_2025.DTOs.RoomDTO;
using WebAPI_2025.Models.Wrappers;
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
            try
            {
                var rooms = await _Service.GetRoomByGuesthouseID(GID);
                return Ok(new APIResponse<List<GetAvailableRoomDTO>>(
                    true,
                    $"Fetched {rooms.Count} rooms for GuestHouse ID {GID}",
                    rooms));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>(false, $"Error: {ex.Message}"));
            }
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableRooms(int guestHouseId, DateTime checkin, DateTime checkout)
        {
            try
            {
                var rooms = await _Service.GetAvailableRooms(guestHouseId, checkin, checkout);
                return Ok(new APIResponse<List<GetAvailableRoomDTO>>(
                    true,
                    $"Available rooms between {checkin:yyyy-MM-dd} and {checkout:yyyy-MM-dd}",
                    rooms));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>(false, $"Error: {ex.Message}"));
            }
        }
    }
}
