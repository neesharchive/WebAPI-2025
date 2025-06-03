using WebAPI_2025.DTOs.UserDTO;

namespace WebAPI_2025.Services
{
    public interface IAuthService
    {
        Task<UserResponseDTO?> Login(LoginDTO loginDTO);
        Task<bool> RequestPasswordReset(string email);
        Task<bool> ResetPassword(string token, string newPassword);
        Task<bool> ChangePassword(int userId, string currentPassword, string newPassword);

    }
}
