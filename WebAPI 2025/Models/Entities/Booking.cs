using WebAPI_2025.Enums;
namespace WebAPI_2025.Models.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public int BedID { get; set; }
        public DateTime checkIn {  get; set; }
        public DateTime checkOut { get; set; }
        public BookingStatus status { get; set; }
        public DateTime CreatedAt { get; private set; }=DateTime.Now;
    }
}
