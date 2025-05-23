using WebAPI_2025.DTOs.RoomDTO;

namespace WebAPI_2025.Services
{
    public interface IRoomService
    {
        public Task<List<GetAvailableRoomDTO>> GetRoomByGuesthouseID(int guesthouseID);
        Task<List<GetAvailableRoomDTO>> GetAvailableRooms(int guestHouseId, DateTime checkin, DateTime checkout);
    }
}
