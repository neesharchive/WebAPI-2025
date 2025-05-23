using WebAPI_2025.DTOs.BookingDTO;
using WebAPI_2025.Models.Entities;

namespace WebAPI_2025.Services
{
    public interface IBookingService
    {
        public Task Create(BookingDTO booking);
        public Task<List<BookingDTO>> GetBookingByUserId(int Uid);
        public Task<BookingDTO?> GetById(int id);
        public void Delete(int id);
        public Task<List<BookingDTO>> GetAll();
        public void Update(int id, BookingDTO booking);

    }
}
