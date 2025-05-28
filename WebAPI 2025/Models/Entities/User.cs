using System.ComponentModel.DataAnnotations;
using WebAPI_2025.Enums;
namespace WebAPI_2025.Models.Entities
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        public required string UserName { get; set; }
        [Required]
        public required string Password { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        public required Roles role { get; set; } = Roles.User;
    }
}
