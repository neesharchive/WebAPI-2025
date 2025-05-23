using AutoMapper;
using WebAPI_2025.DTOs.BedDTO;
using WebAPI_2025.DTOs.RoomDTO;
using WebAPI_2025.Models.Entities;
using WebAPI_2025.Repositories;

namespace WebAPI_2025.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _repo;
        private readonly IMapper _mapper;
        public RoomService(IRoomRepository roomRepository, IMapper mapper)
        {
            _repo = roomRepository;
            _mapper = mapper;
        }
        public async Task<List<GetAvailableRoomDTO>> GetRoomByGuesthouseID(int guesthouseID)
        {
            var room=await _repo.GetRoomByGuesthouseID(guesthouseID);
            return _mapper.Map<List<GetAvailableRoomDTO>>(room);
        }
        public async Task<List<GetAvailableRoomDTO>> GetAvailableRooms(int guestHouseId, DateTime checkin, DateTime checkout)
        {
            var rooms = await _repo.GetAvaiablebRoom(guestHouseId,checkin,checkout);
            return _mapper.Map<List<GetAvailableRoomDTO>>(rooms);
        }
    }
}
