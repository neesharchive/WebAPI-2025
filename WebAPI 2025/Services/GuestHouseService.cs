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
                dto.Location = gh.Location;

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
            guesthouse.Name = dto.Name;

            await _context.SaveChangesAsync();
        }

        public async Task<List<string>> GetAllLocationsAsync()
        {
            return await _repo.GetAllLocationsAsync();
        }

        public async Task<List<GetGuestHouseDTO>> GetGuestHousesByLocationAsync(string location)
        {
            var guesthouses = await _repo.GetGuestHousesByLocationAsync(location);

            if (guesthouses == null || guesthouses.Count == 0)
                return new List<GetGuestHouseDTO>();

            var dtoList = new List<GetGuestHouseDTO>();

            foreach (var gh in guesthouses)
            {
                // Count number of rooms for this guest house
                var rooms = await _context.rooms
                    .Where(r => r.guesthouseID == gh.GuestHouseID)
                    .ToListAsync();

                int totalBeds = 0;
                foreach (var room in rooms)
                {
                    totalBeds += await _context.beds.CountAsync(b => b.RoomID == room.RoomID);
                }

                dtoList.Add(new GetGuestHouseDTO
                {
                    Name = gh.Name,
                    Location = gh.Location,
                    Status = gh.Status,
                    NumberOfRooms = rooms.Count,
                    BedsPerRoom = rooms.FirstOrDefault()?.Capacity ?? 0,
                    GuestHouseID=gh.GuestHouseID
                }) ;
            }

            return dtoList;
        }

        public async Task<List<GetGuestHouseDTO>> GetAvailableGuestHousesByLocationAsync(string location, DateTime checkIn, DateTime checkOut)
        {
            var guesthouses = await _repo.GetGuestHousesByLocationAsync(location);
            var availableGuestHouses = new List<GetGuestHouseDTO>();

            foreach (var gh in guesthouses)
            {
                var rooms = await _context.rooms
                    .Where(r => r.guesthouseID == gh.GuestHouseID)
                    .ToListAsync();

                bool hasAvailableRoom = false;

                foreach (var room in rooms)
                {
                    var beds = await _context.beds
                        .Where(b => b.RoomID == room.RoomID)
                        .ToListAsync();

                    foreach (var bed in beds)
                    {
                        bool isBooked = await _context.bookings.AnyAsync(book =>
                            book.BedID == bed.BedID &&
                            !(book.CheckOutDate <= checkIn || book.CheckInDate >= checkOut));

                        if (!isBooked)
                        {
                            hasAvailableRoom = true;
                            break;
                        }
                    }

                    if (hasAvailableRoom) break;
                }

                if (hasAvailableRoom)
                {
                    availableGuestHouses.Add(new GetGuestHouseDTO
                    {
                        Name = gh.Name,
                        Location = gh.Location,
                        Status = gh.Status,
                        NumberOfRooms = rooms.Count,
                        BedsPerRoom = rooms.FirstOrDefault()?.Capacity ?? 0,
                        GuestHouseID = gh.GuestHouseID
                    });
                }
            }

            return availableGuestHouses;
        }

    }


}
