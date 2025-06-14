﻿using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebAPI_2025.Data;
using WebAPI_2025.Models.Entities;

namespace WebAPI_2025.Repositories
{
    public class GuestHouseRepository : IGuestHouseRepository
    {
        private readonly AppDbContext _context;
        public GuestHouseRepository(AppDbContext appDbContext)
        {
            _context= appDbContext;
        }
        public async Task Create(GuestHouse guesthouse)
        {
            await _context.guestHouses.AddAsync(guesthouse);

            await _context.SaveChangesAsync();
        }

        public void Delete(GuestHouse guestHouse)
        {
            _context.guestHouses.Remove(guestHouse);
            _context.SaveChanges();
        }

        public async Task<GuestHouse?> Get(int id)
        {
            return await _context.guestHouses.Where(x=>x.GuestHouseID==id).FirstOrDefaultAsync();
        }

        public async Task<List<GuestHouse>> GetAll()
        {
            return await _context.guestHouses.ToListAsync();
        }
        public async Task<List<string>> GetAllLocationsAsync()
        {
            return await _context.guestHouses
                .Select(gh => gh.Location)
                .Distinct()
                .ToListAsync();
        }

        public async Task<List<GuestHouse>> GetGuestHousesByLocationAsync(string location)
        {
            return await _context.guestHouses
                .Where(gh => gh.Location.ToLower() == location.ToLower())
                .ToListAsync();
        }

    }
}
  