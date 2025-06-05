using WebAPI_2025.Enums;

namespace WebAPI_2025.DTOs.GuestHouseDTO
{
    public class GetGuestHouseDTO
    {
        public required int GuestHouseID {  get; set; }
        public required string Name { get; set; }
        public required string Location { get; set; }
        public GH_Status Status { get; set; }
        public int NumberOfRooms { get; set; }
        public int BedsPerRoom { get; set; }
    }
}
