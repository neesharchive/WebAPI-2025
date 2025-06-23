using WebAPI_2025.DTOs.NotificationDTO;
using WebAPI_2025.Enums;
using WebAPI_2025.Models.Entities;
using WebAPI_2025.Repositories;

namespace WebAPI_2025.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repo;

        public NotificationService(INotificationRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<NotificationDTO>> GetForUser(int userId)
        {
            var user = await _repo.GetUserById(userId);
            if (user == null) return new List<NotificationDTO>();

            var userNotifs = await _repo.GetForUser(userId);
            var roleNotifs = await _repo.GetForRole(user.role);

            return userNotifs
                .Concat(roleNotifs)
                .OrderByDescending(n => n.Timestamp)
                .Select(n => new NotificationDTO
                {
                    NotificationID = n.NotificationID,
                    Message = n.Message,
                    Timestamp = n.Timestamp,
                    IsRead = n.IsRead
                }).ToList();
        }

        public async Task<List<NotificationDTO>> GetForRole(Roles role)
        {
            var data = await _repo.GetForRole(role);
            return data.Select(n => new NotificationDTO
            {
                NotificationID = n.NotificationID,
                Message = n.Message,
                Timestamp = n.Timestamp,
                IsRead = n.IsRead
            }).ToList();
        }

        public async Task MarkAsRead(int id)
        {
            await _repo.MarkAsRead(id);
            await _repo.Save();
        }

        public async Task CreateNotification(Notification notification)
        {
            await _repo.Add(notification);
            await _repo.Save();
        }
    }
}
