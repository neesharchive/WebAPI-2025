using WebAPI_2025.Models.Entities;

namespace WebAPI_2025.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByResetTokenAsync(string token);
        Task SaveChangesAsync();
    }

}
