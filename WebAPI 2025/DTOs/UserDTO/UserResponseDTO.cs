using Azure.Identity;
using WebAPI_2025.Enums;

namespace WebAPI_2025.DTOs.UserDTO
{
    public class UserResponseDTO
    {
        public required string Token {  get; set; }
        public required string Role { get; set; }
        public required int UserID { get; set; }
    }
}
