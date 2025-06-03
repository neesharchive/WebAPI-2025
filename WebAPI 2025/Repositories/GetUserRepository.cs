using Microsoft.EntityFrameworkCore;
using WebAPI_2025.Data;
using WebAPI_2025.Models.Entities;

namespace WebAPI_2025.Repositories
{
    public class GetUserRepository : IGetUserRepositoroy
    {
        private readonly AppDbContext _db;
        public GetUserRepository(AppDbContext appDbContext)
        {
            _db = appDbContext;
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.UserID == id);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserByResetToken(string token)
        {
            return await _db.Users.FirstOrDefaultAsync(u =>
                u.PasswordResetToken == token && u.TokenExpiryTime > DateTime.UtcNow);
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync() > 0;
        }
    }
}
