using Microsoft.EntityFrameworkCore;
using WebAPI_2025.Data;
using WebAPI_2025.Enums;
using WebAPI_2025.Models.Entities;

namespace WebAPI_2025.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext _db;

        public NotificationRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Notification>> GetForUser(int userId)
        {
            return await _db.notification
                .Where(n => n.UserID == userId)
                .OrderByDescending(n => n.Timestamp)
                .ToListAsync();
        }

        public async Task<List<Notification>> GetForRole(Roles role)
        {
            return await _db.notification
                .Where(n => n.TargetRole == role)
                .OrderByDescending(n => n.Timestamp)
                .ToListAsync();
        }

        public async Task<User?> GetUserById(int userId)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.UserID == userId);
        }

        public async Task MarkAsRead(int id)
        {
            var notif = await _db.notification.FindAsync(id);
            if (notif != null)
            {
                notif.IsRead = true;
            }
        }

        public async Task Add(Notification notification)
        {
            await _db.notification.AddAsync(notification);
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
