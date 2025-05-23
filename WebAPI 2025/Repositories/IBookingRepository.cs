using WebAPI_2025.DTOs.BookingDTO;
using WebAPI_2025.Models.Entities;

namespace WebAPI_2025.Repositories
{
    public interface IBookingRepository
    {
        public Task Create(Booking booking);
        public Task<List<Booking>> GetByUserID(int id);
        public Task<Booking> GetByID(int id);
        public Task<List<Booking>> GetAll();
        public void Update(Booking booking);
        public void Delete(Booking booking);

    }
}
