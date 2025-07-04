﻿using WebAPI_2025.Models.Entities;
using WebAPI_2025.DTOs.GuestHouseDTO;
namespace WebAPI_2025.Services
{
    public interface IGuestHouseService
    {
        public Task<List<GetGuestHouseDTO>> GetAllAsync();
        public Task<GetGuestHouseDTO?> GetAsync(int id);
        public Task CreateAsync(AddGuestHouseDTO guesthouse);
        public void Delete(int id);
        Task UpdateGuestHouseAsync(UpdateGuestHouseDTO dto);
        Task<List<string>> GetAllLocationsAsync();
        Task<List<GetGuestHouseDTO>> GetGuestHousesByLocationAsync(string location);
        Task<List<GetGuestHouseDTO>> GetAvailableGuestHousesByLocationAsync(string location, DateTime checkIn, DateTime checkOut);
        
    }
}
