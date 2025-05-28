using Microsoft.Identity.Client;
using WebAPI_2025.Models.Entities;
namespace WebAPI_2025.Repositories
{
    public interface IGetUser
    {
        public Task<User?> GetUserByUsernamePassword(string username, string password);
    }
}
