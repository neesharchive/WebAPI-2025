using System.ComponentModel.DataAnnotations;
using WebAPI_2025.Enums;
namespace WebAPI_2025.Models.Entities
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Roles role { get; set; }
    }
}
