using Microsoft.EntityFrameworkCore;
using WebAPI_2025.Data;
using WebAPI_2025.Models.Entities;

namespace WebAPI_2025.Repositories
{
    public class BedRepository : IBedRepository
    {
        private readonly AppDbContext _appDb;
        public BedRepository(AppDbContext appDb)
        {
            _appDb = appDb;
        }
        public async Task Create(Bed bed)
        {
            await _appDb.beds.AddAsync(bed);
            await _appDb.SaveChangesAsync();
        }

        public async Task<List<Bed>> GetByRoomID(int roomID)
        {
            return await _appDb.beds
                .Where(b=>b.RoomID == roomID)
                .ToListAsync();

        }
        public async Task<List<Bed>> GetAvaiablebBeds(int roomID, DateTime CID, DateTime COD)
        {
            return await _appDb.beds
                .Where(b => b.RoomID == roomID &&
                    !_appDb.bookings.Any(book =>
                        book.BedID == b.BedID &&
                        !(book.CheckOutDate <= CID || book.CheckInDate >= COD)))
                .ToListAsync();
        }
    }
}
