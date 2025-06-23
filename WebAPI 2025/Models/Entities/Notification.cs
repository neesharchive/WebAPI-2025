using System.ComponentModel.DataAnnotations;
using WebAPI_2025.Enums; // Assuming your UserRole enum is here
namespace WebAPI_2025.Models.Entities
{
    public class Notification
    {
        [Key]
        public int NotificationID { get; set; }
        public int? UserID { get; set; } // Null means it targets role instead
        public string Message { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; } = false;

        public Roles? TargetRole { get; set; } // Null = user-specific
    }

}
