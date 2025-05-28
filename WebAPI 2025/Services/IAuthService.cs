using WebAPI_2025.DTOs.UserDTO;

namespace WebAPI_2025.Services
{
    public interface IAuthService
    {
        public Task<UserResponseDTO?> Login(LoginDTO loginDTO);
    }
}
