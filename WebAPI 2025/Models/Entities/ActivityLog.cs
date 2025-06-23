namespace WebAPI_2025.Models.Entities
{
    public class ActivityLog
    {
        public int Id { get; set; }
        public int? UserId { get; set; }  // Optional for system or admin actions
        public string Action { get; set; }  // e.g. "Login", "Booking Created", "Booking Updated", etc.
        public string Details { get; set; }  // JSON or readable string describing the action
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

}
