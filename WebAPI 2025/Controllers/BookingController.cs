using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_2025.DTOs.BookingDTO;
using WebAPI_2025.Services;

namespace WebAPI_2025.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _Service;
        public BookingController(IBookingService bookingService)
        {
            _Service = bookingService;
        }
        [HttpPost]
        public async Task<IActionResult> Create(BookingDTO DTO)
        {
            await _Service.Create(DTO);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookings = await _Service.GetAll();
            return Ok(bookings);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetbyID(int Id)
        {
            var booking = _Service.GetById(Id);
            return booking == null ? NotFound() : Ok(booking);
        }
        [HttpGet("user/{UID}")]
        public async Task<IActionResult> GetbyUser(int UID)
        {
            var booking = _Service.GetById(UID);
            return booking == null ? NotFound() : Ok(booking);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _Service.Delete(id);
            return NoContent();
        }
    }
}
