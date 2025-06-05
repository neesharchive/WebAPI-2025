using WebAPI_2025.Enums;

namespace WebAPI_2025.DTOs.GuestHouseDTO
{
    public class UpdateGuestHouseDTO
    {
        public int GuestHouseID { get; set; }
        public string Name { get; set; }
        public GH_Status Status { get; set; }
    }

}
