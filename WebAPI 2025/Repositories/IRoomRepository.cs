using WebAPI_2025.Models.Entities;
namespace WebAPI_2025.Repositories
{
    public interface IRoomRepository
    {
        public Task Create(Room room);
        public Task<List<Room>> GetRoomByGuesthouseID(int guesthouseID);
        public Task<List<Room>> GetAvaiablebRoom(int GuestHouseID, DateTime CID, DateTime COD);

    }
}
