using WebAPI_2025.Enums;

namespace WebAPI_2025.DTOs.GuestHouseDTO
{
    public class AddGuestHouseDTO
    {
        public required string GH_Name { get; set; }
        public required string GH_Location { get; set; }
        public GH_Status Status { get; set; } = GH_Status.Active;

    }
}
