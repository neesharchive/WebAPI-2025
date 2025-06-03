using Microsoft.AspNetCore.Mvc;
using WebAPI_2025.DTOs.PasswordDTO;
using WebAPI_2025.DTOs.UserDTO;
using WebAPI_2025.Models.Wrappers;
using WebAPI_2025.Services;

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
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            try
            {
                var result = await _authService.Login(dto);
                if (result == null)
                    return Unauthorized(new APIResponse<string>(false, "Invalid Username or Password"));

                return Ok(new APIResponse<UserResponseDTO>(true, "Login Successful", result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>(false, $"Login failed: {ex.Message}"));
            }
        }

        [HttpPost("request-reset")]
        public async Task<IActionResult> RequestResetByEmail([FromBody] ForgotPasswordDTO dto)
        {
            try
            {
                var success = await _authService.RequestPasswordReset(dto.Email);
                if (!success)
                    return NotFound(new APIResponse<string>(false, "Email not found"));

                return Ok(new APIResponse<string>(true, "Reset link sent to your registered email."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>(false, $"Failed to send reset link: {ex.Message}"));
            }
        }


        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO dto)
        {
            try
            {
                var success = await _authService.ResetPassword(dto.Token, dto.NewPassword);
                if (!success)
                    return BadRequest(new APIResponse<string>(false, "Invalid or expired token"));

                return Ok(new APIResponse<string>(true, "Password reset successful"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>(false, $"Password reset failed: {ex.Message}"));
            }
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO dto)
        {
            try
            {
                var success = await _authService.ChangePassword(dto.UserID, dto.CurrentPassword, dto.NewPassword);
                if (!success)
                    return BadRequest(new APIResponse<string>(false, "Current password is incorrect"));

                return Ok(new APIResponse<string>(true, "Password changed successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>(false, $"Password change failed: {ex.Message}"));
            }
        }
    }
}
