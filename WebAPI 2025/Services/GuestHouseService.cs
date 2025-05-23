using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using WebAPI_2025.Data;
using WebAPI_2025.DTOs.GuestHouseDTO;
using WebAPI_2025.Models.Entities;
using WebAPI_2025.Repositories;
namespace WebAPI_2025.Services
{
    public class GuestHouseService : IGuestHouseService
    {
        private readonly IGuestHouseRepository _repo;
        //private readonly IBedRepository bedRepository;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public GuestHouseService(IGuestHouseRepository guestHouseRepository, IMapper mapper, AppDbContext appDbContext)
        {
            _repo= guestHouseRepository;
            _mapper= mapper;
            _context= appDbContext;
        }
        public async Task CreateAsync(AddGuestHouseDTO AddGH)
        {
            var entity = _mapper.Map<GuestHouse>(AddGH);

            await _context.guestHouses.AddAsync(entity);
            await _context.SaveChangesAsync();

            for (int i=1; i<=AddGH.NumberOfRooms; i++)
            {
                var room = new Room
                {
                    RoomNumber = i,
                    Capacity = AddGH.BedsPerRoom,
                    guesthouseID = entity.GuestHouseID
                };
                await _context.AddAsync(room);
                await _context.SaveChangesAsync();

                for(int j=1;j<=AddGH.BedsPerRoom; j++)
                {
                    var bed = new Bed
                    {
                        BedNumber = j,
                        RoomID = room.RoomID
                    };
                    await _context.AddAsync(bed);
                }
            }
            await _context.SaveChangesAsync();

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
            var guestHouses = await _repo.GetAll();
            var dtos = new List<GetGuestHouseDTO>();

            foreach (var gh in guestHouses)
            {
                var rooms = await _context.rooms
                    .Where(r => r.guesthouseID == gh.GuestHouseID)
                    .ToListAsync();

                int numberOfRooms = rooms.Count;
                int bedsPerRoom = numberOfRooms > 0
                    ? await _context.beds.CountAsync(b => b.RoomID == rooms.First().RoomID)
                    : 0;

                var dto = _mapper.Map<GetGuestHouseDTO>(gh);
                dto.NumberOfRooms = numberOfRooms;
                dto.BedsPerRoom = bedsPerRoom;

                dtos.Add(dto);
            }

            return dtos;
        }


        public async Task<GetGuestHouseDTO?> GetAsync(int id)
        {
            var guestHouse = await _repo.Get(id);
            if (guestHouse == null) return null;

            var rooms = await _context.rooms
                .Where(r => r.guesthouseID == guestHouse.GuestHouseID)
                .ToListAsync();

            int numberOfRooms = rooms.Count;
            int bedsPerRoom = numberOfRooms > 0
                ? await _context.beds.CountAsync(b => b.RoomID == rooms.First().RoomID)
                : 0;

            var dto = _mapper.Map<GetGuestHouseDTO>(guestHouse);
            dto.NumberOfRooms = numberOfRooms;
            dto.BedsPerRoom = bedsPerRoom;

            return dto;
        }
        public async Task UpdateGuestHouseAsync(UpdateGuestHouseDTO dto)
        {
            var guesthouse = await _repo.Get(dto.GuestHouseID);
            if (guesthouse == null) return;

            guesthouse.Status = dto.Status;

            // Get existing rooms
            var existingRooms = await _context.rooms
                .Where(r => r.guesthouseID == guesthouse.GuestHouseID)
                .ToListAsync();

            int currentRooms = existingRooms.Count;

            if (dto.NumberOfRooms > currentRooms)
            {
                // Add new rooms and beds
                for (int i = currentRooms + 1; i <= dto.NumberOfRooms; i++)
                {
                    var room = new Room
                    {
                        RoomNumber = i,
                        Capacity = dto.BedsPerRoom,
                        guesthouseID = guesthouse.GuestHouseID
                    };
                    await _context.rooms.AddAsync(room);
                    await _context.SaveChangesAsync(); // To get RoomID

                    for (int j = 1; j <= dto.BedsPerRoom; j++)
                    {
                        await _context.beds.AddAsync(new Bed
                        {
                            BedNumber = j,
                            RoomID = room.RoomID
                        });
                    }
                }
            }

            await _context.SaveChangesAsync();
        }



    }
}
