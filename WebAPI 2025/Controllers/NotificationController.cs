using Microsoft.AspNetCore.Mvc;
using WebAPI_2025.DTOs.NotificationDTO;
using WebAPI_2025.Enums;
using WebAPI_2025.Models.Wrappers;
using WebAPI_2025.Services;

namespace WebAPI_2025.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _service;

        public NotificationController(INotificationService service)
        {
            _service = service;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<NotificationDTO>>> GetForUser(int userId)
        {
            var list = await _service.GetForUser(userId);
            return Ok(new APIResponse<List<NotificationDTO>> { Success = true, Data = list });
        }

        [HttpGet("role/{role}")]
        public async Task<ActionResult<List<NotificationDTO>>> GetForRole(Roles role)
        {
            var list = await _service.GetForRole(role);
            return Ok(new APIResponse<List<NotificationDTO>> { Success = true, Data = list });
        }

        [HttpPut("mark-as-read/{id}")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            await _service.MarkAsRead(id);
            return Ok(new APIResponse<string> { Success = true, Message = "Marked as read" });
        }

    }
}
