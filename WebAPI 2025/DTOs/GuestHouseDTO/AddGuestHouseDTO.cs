using WebAPI_2025.Enums;

namespace WebAPI_2025.DTOs.GuestHouseDTO
{
    public class AddGuestHouseDTO
    {
        public required string Name { get; set; }
        public required string Location { get; set; }
        public GH_Status Status { get; set; } = GH_Status.Active;
        public int NumberOfRooms { get; set; }
        public int BedsPerRoom { get; set; }      
    }
}
