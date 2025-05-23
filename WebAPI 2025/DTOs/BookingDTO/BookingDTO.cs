using WebAPI_2025.Enums;

namespace WebAPI_2025.DTOs.BookingDTO
{
    public class BookingDTO
    {
        public int BookingID { get; set; }// Used for GET, not saved
        public int UserID { get; set; }
        public int BedID { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckoutDate { get; set; }
        public string Purpose { get; set; }
        public Gender Gender { get; set; }
        public BookingStatus Status { get; set; }=BookingStatus.Pending;

        // These are only for display, not mapped
        public int RoomNumber { get; set; }
        public int BedNumber { get; set; }
        public string GuestHouseName { get; set; }
        public string Location { get; set; }
    }

}
