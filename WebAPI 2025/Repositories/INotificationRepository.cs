using WebAPI_2025.Models.Entities;
using WebAPI_2025.Enums;
namespace WebAPI_2025.Repositories
{
    public interface INotificationRepository
    {
        Task<List<Notification>> GetForUser(int userId);
        Task<List<Notification>> GetForRole(Roles role);
        Task MarkAsRead(int id);
        Task Add(Notification notification);
        Task Save();
        Task<User?> GetUserById(int userId);
    }
}
