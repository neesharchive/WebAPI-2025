using System.ComponentModel.DataAnnotations;
using WebAPI_2025.Enums;
namespace WebAPI_2025.Models.Entities
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        public string UserName { get; set; }=string.Empty;
        [Required]
        public string Password { get; set; }=string.Empty;
        [Required]
        public string Email { get; set; }=string.Empty;
        [Required]
        public Roles role { get; set; } = Roles.User;
        public string? PasswordResetToken { get; set; }

        public DateTime? TokenExpiryTime { get; set; }
    }
}
