using WebAPI_2025.Models.Entities;

namespace WebAPI_2025.Repositories
{
    public interface IGuestHouseRepository
    {
        public Task<List<GuestHouse>> GetAll();
        public Task<GuestHouse?>Get(int id);
        public Task Create(GuestHouse guesthouse);
        public void Delete(GuestHouse guestHouse);
    }
}
