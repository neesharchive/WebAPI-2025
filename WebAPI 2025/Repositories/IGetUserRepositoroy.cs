using WebAPI_2025.Models.Entities;

namespace WebAPI_2025.Repositories
{
    public interface IGetUserRepositoroy
    {
        Task<User?> GetUserById(int id);
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserByResetToken(string token);
        Task<User?> GetUserByUsername(string username);
        Task<bool> SaveChangesAsync();
    }
}
