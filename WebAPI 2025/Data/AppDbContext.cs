using Microsoft.EntityFrameworkCore;
using WebAPI_2025.Models.Entities;
using WebAPI_2025.Models;
using WebAPI_2025.Enums;
using Microsoft.AspNetCore.Identity;
namespace WebAPI_2025.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<GuestHouse> guestHouses{ get; set; }
        public DbSet<Room> rooms { get; set; }
        public DbSet<Bed> beds { get; set; }
        public DbSet<Booking> bookings { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }

        public DbSet<Notification> notification { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cascade delete relationships (unchanged)
            modelBuilder.Entity<Room>()
                .HasOne<GuestHouse>()
                .WithMany()
                .HasForeignKey(r => r.guesthouseID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Bed>()
                .HasOne<Room>()
                .WithMany()
                .HasForeignKey(b => b.RoomID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Booking>()
                .HasOne<Bed>()
                .WithMany()
                .HasForeignKey(b => b.BedID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Booking>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(b => b.UserID)
                .OnDelete(DeleteBehavior.NoAction);


            var hasher = new PasswordHasher<User>();

            var adminUser = new User
            {
                UserID = 1,
                UserName = "admin",
                Email = "nishantbhatt393@gmail.com",
                role = Roles.Admin
            };
            adminUser.Password = hasher.HashPassword(adminUser, "admin123");

            var normalUser = new User
            {
                UserID = 2,
                UserName = "user",
                Email = "nab5996@psu.com",
                role = Roles.User
            };
            normalUser.Password = hasher.HashPassword(normalUser, "user123");

            modelBuilder.Entity<User>().HasData(adminUser, normalUser);


        }


    }
}
