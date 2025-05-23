using WebAPI_2025.DTOs.BedDTO;
using WebAPI_2025.Models.Entities;
namespace WebAPI_2025.Services
{
    public interface IBedService
    {
        public Task<List<GetAvailableBedDTO>> GetBedByRoomID(int roomID);
        public Task<List<GetAvailableBedDTO>> GetAvailableBeds(int roomId, DateTime checkin, DateTime checkout);
    }
}
