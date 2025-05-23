using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Identity.Client;

namespace WebAPI_2025.DTOs.RoomDTO
{
    public class GetAvailableRoomDTO
    {
        public int RoomID { get; set; }
        public int RoomNumber { get; set; }
        public int Capacity {  get; set; }
        public int GuestHouseId { get; set; }
    }
}
