using AutoMapper;
using WebAPI_2025.DTOs.BedDTO;
using WebAPI_2025.Models.Entities;
using WebAPI_2025.Repositories;

namespace WebAPI_2025.Services
{
    public class BedService : IBedService
    {
        private readonly  IBedRepository _repo;
        private readonly IMapper _mapper;
        public BedService(IBedRepository bedRepository, IMapper mapper)
        {
            _repo = bedRepository;
            _mapper = mapper;
            
        }

        public async Task<List<GetAvailableBedDTO>> GetBedByRoomID(int roomID)
        {
            var beds= await _repo.GetByRoomID(roomID);
            return _mapper.Map<List<GetAvailableBedDTO>>(beds);
            
        }
        public async Task<List<GetAvailableBedDTO>> GetAvailableBeds(int roomId, DateTime checkin, DateTime checkout)
        {
            var beds = await _repo.GetAvaiablebBeds(roomId, checkin, checkout);
            return _mapper.Map<List<GetAvailableBedDTO>>(beds);
        }
    }
}
