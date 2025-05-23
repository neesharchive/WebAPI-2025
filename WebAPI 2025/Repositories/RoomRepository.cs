using Microsoft.EntityFrameworkCore;
using WebAPI_2025.Data;
using WebAPI_2025.Models.Entities;

namespace WebAPI_2025.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly AppDbContext _db;
        public RoomRepository(AppDbContext appDbContext)
        {
            _db = appDbContext;
        }
        public async Task Create(Room room)
        {
            await _db.rooms.AddAsync(room);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Room>> GetRoomByGuesthouseID(int GHID)
        {
            return await _db.rooms
                .Where(r => r.guesthouseID == GHID)
                .ToListAsync();
        }
        public async Task<List<Room>> GetAvaiablebRoom(int GuestHouseID, DateTime CID, DateTime COD)
        {
            return await _db.rooms
                 .Where(r => r.guesthouseID == GuestHouseID &&
                     _db.beds.Any(b => b.RoomID == r.RoomID &&
                         !_db.bookings.Any(book =>
                             book.BedID == b.BedID &&
                             !(book.CheckOutDate <= COD|| book.CheckInDate >= CID))))
                 .ToListAsync();
        }

    }
}
