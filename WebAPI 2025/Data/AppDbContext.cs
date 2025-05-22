using Microsoft.EntityFrameworkCore;
using WebAPI_2025.Models.Entities;

namespace WebAPI_2025.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }
        public DbSet<GuestHouse> guestHouses{ get; set; }
        public DbSet<Room> rooms { get; set; }
        public DbSet<Bed> beds { get; set; }
        public DbSet<Booking> bookings { get; set; }
    }
}
