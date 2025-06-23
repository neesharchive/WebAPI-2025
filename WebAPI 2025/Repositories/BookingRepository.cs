using Microsoft.EntityFrameworkCore;
using WebAPI_2025.Data;
using WebAPI_2025.Models.Entities;

namespace WebAPI_2025.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _db;

        public BookingRepository(AppDbContext appDbContext)
        {
            _db = appDbContext;
        }

        public async Task Create(Booking booking)
        {
            await _db.AddAsync(booking);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Booking booking)
        {
            _db.Remove(booking);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Booking>> GetAll()
        {
            return await _db.bookings.ToListAsync();
        }

        public async Task<Booking> GetByID(int id)
        {
            return await _db.bookings.FindAsync(id);
        }

        public async Task<List<Booking>> GetByUserID(int id)
        {
            return await _db.bookings.Where(b => b.UserID == id).ToListAsync();
        }

        public async Task Update(Booking booking)
        {
            _db.bookings.Update(booking);
            await _db.SaveChangesAsync();
        }
    }
}
