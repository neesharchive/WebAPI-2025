using System.ComponentModel.DataAnnotations;
using WebAPI_2025.Enums;
namespace WebAPI_2025.Models.Entities
{
    public class Booking
    {
        [Key]
        public int BookingID { get; set; }
        public int UserID { get; set; }
        public int BedID { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string Purpose { get; set; }
        public Gender Gender { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }

}
