using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_2025.Services;

namespace WebAPI_2025.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BedController : ControllerBase
    {
        private readonly IBedService _Service;
        public BedController(IBedService bedServices)
        {
            _Service = bedServices;

        }

        [HttpGet("room/{roomID}")]
        public async Task<IActionResult> GetBedsByRoomID(int roomID)
        {
            var beds = await _Service.GetBedByRoomID(roomID);
            return Ok(beds);
        }

        // GET: api/bed/available?roomId=3&checkin=...&checkout=...
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableBeds(int roomId, DateTime checkin, DateTime checkout)
        {
            var beds = await _Service.GetAvailableBeds(roomId, checkin, checkout);
            return Ok(beds);
        }
    }
}
