using WebAPI_2025.Enums;

namespace WebAPI_2025.DTOs.BookingDTO
{
    public class BookingUpdateDTO
    {
        public int BookingID { get; set; }
        public int UserID { get; set; }
        public int BedID { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckoutDate { get; set; }
        public string Purpose { get; set; }
        public Gender Gender { get; set; }
    }
}
