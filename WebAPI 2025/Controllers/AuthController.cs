using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_2025.DTOs.UserDTO;
using WebAPI_2025.Services;
using WebAPI_2025.Models.Wrappers;
namespace WebAPI_2025.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("login")]

        public async Task<IActionResult> Login([FromBody] LoginDTO dto){
            try
            {
                var result = await _authService.Login(dto);
                if (result == null)
                {
                    return Unauthorized(new APIResponse<string>(false, "Invalid Username or Password"));
                }
                return Ok(new APIResponse<UserResponseDTO>(true, "Login Successful", result));
            }catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>(false, $"Login failed: {ex.Message}"));
            }
        }
    }
}
