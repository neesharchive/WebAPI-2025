using Microsoft.EntityFrameworkCore;
using WebAPI_2025.Data;
using WebAPI_2025.Models.Entities;

namespace WebAPI_2025.Repositories
{
    public class GetUser : IGetUser
    {
        private readonly AppDbContext _db;
        public GetUser(AppDbContext appDbContext)
        {
            _db = appDbContext;
        }
        public async Task<User?> GetUserByUsernamePassword(string username, string password)
        {
            return await _db.Users.FirstOrDefaultAsync(u=>u.UserName == username && u.Password == password);
        }
    }
}
