using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebAPI_2025.Data;
using WebAPI_2025.Models.Entities;

namespace WebAPI_2025.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDb;
        public UserRepository(AppDbContext appDbContext)
        {
            _appDb = appDbContext;
        }
        public async Task<User?> GetByResetTokenAsync(string token)
        {
            return await _appDb.Users.FirstOrDefaultAsync(u=>u.PasswordResetToken == token 
            && u.TokenExpiryTime>DateTime.UtcNow);
        }

        public Task<User?> GetByUsernameAsync(string username)
        {
            return _appDb.Users.FirstOrDefaultAsync(u=>u.UserName == username);
        }

        public async Task SaveChangesAsync()
        {
            await _appDb.SaveChangesAsync();
        }
    }
}
