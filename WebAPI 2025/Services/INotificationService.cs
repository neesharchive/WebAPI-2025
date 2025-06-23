using System.Data;
using WebAPI_2025.DTOs;
using WebAPI_2025.DTOs.NotificationDTO;
using WebAPI_2025.Enums;
using WebAPI_2025.Models.Entities;

namespace WebAPI_2025.Services
{
    public interface INotificationService
    {
        Task<List<NotificationDTO>> GetForUser(int userId);
        Task<List<NotificationDTO>> GetForRole(Roles role);
        Task MarkAsRead(int id);
        Task CreateNotification(Notification notification);
    }
}
