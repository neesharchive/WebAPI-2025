using Microsoft.EntityFrameworkCore;
using WebAPI_2025.Models.Entities;
using WebAPI_2025.Models;
namespace WebAPI_2025.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }
        public DbSet<GuestHouse> guestHouses{ get; set; }
        public DbSet<Room> rooms { get; set; }
        public DbSet<Bed> beds { get; set; }
        public DbSet<Booking> bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // GuestHouse -> Room (Cascade delete)
            modelBuilder.Entity<Room>()
                .HasOne<GuestHouse>()
                .WithMany()
                .HasForeignKey(r => r.guesthouseID)
                .OnDelete(DeleteBehavior.Cascade);

            // Room -> Bed (Cascade delete)
            modelBuilder.Entity<Bed>()
                .HasOne<Room>()
                .WithMany()
                .HasForeignKey(b => b.RoomID)
                .OnDelete(DeleteBehavior.Cascade);

            // Bed -> Booking (No cascade, just FK)
            modelBuilder.Entity<Booking>()
                .HasOne<Bed>()
                .WithMany()
                .HasForeignKey(b => b.BedID)
                .OnDelete(DeleteBehavior.NoAction); // optional safety

            // Optional: Booking -> User FK
            modelBuilder.Entity<Booking>()
                .HasOne<User>() 
                .WithMany()
                .HasForeignKey(b => b.UserID)
                .OnDelete(DeleteBehavior.NoAction);
        }

    }
}
