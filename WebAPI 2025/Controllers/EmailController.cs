using Microsoft.AspNetCore.Mvc;
using WebAPI_2025.Services;

namespace WebAPI_2025.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailTestController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailTestController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet("test-email")]
        public IActionResult TestEmail()
        {
            try
            {
                _emailService.SendEmailInBackground("your-email@gmail.com", "Test Email", "This is a test from the Guest House system.");
                return Ok("Email triggered successfully (sent in background).");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to trigger email: {ex.Message}");
            }
        }
    }
}
