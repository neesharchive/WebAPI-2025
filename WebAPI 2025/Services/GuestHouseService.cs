using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using WebAPI_2025.DTOs.GuestHouseDTO;
using WebAPI_2025.Models.Entities;
using WebAPI_2025.Repositories;
namespace WebAPI_2025.Services
{
    public class GuestHouseService : IGuestHouseService
    {
        private readonly IGuestHouseRepository _repo;
        private readonly IMapper _mapper;
        public GuestHouseService(IGuestHouseRepository guestHouseRepository, IMapper mapper)
        {
            _repo= guestHouseRepository;
            _mapper= mapper;
        }
        public async Task CreateAsync(AddGuestHouseDTO AddGH)
        {
            var entity=_mapper.Map<GuestHouse>(AddGH);
            await _repo.Create(entity);
        }

        public void Delete(int id)
        {
            var guestHouse=_repo.Get(id).Result;
            if (guestHouse != null)
            {
                _repo.Delete(guestHouse);
            }
            
        }

        public async Task<List<GetGuestHouseDTO>> GetAllAsync()
        {
            var guesthouselist= await _repo.GetAll();
            var DTO_List= _mapper.Map<List<GetGuestHouseDTO>>(guesthouselist);
            return DTO_List;

        }

        public async Task<GetGuestHouseDTO> GetAsync(int id)
        {
            var GHID= await _repo.Get(id);
            var dto= _mapper.Map<GetGuestHouseDTO>(GHID);
            return dto;
        }
    }
}
