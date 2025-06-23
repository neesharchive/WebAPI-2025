namespace WebAPI_2025.DTOs.NotificationDTO
{
        public class NotificationDTO
        {
            public int NotificationID { get; set; }
            public string Message { get; set; }
            public DateTime Timestamp { get; set; }
            public bool IsRead { get; set; }
        }
}


