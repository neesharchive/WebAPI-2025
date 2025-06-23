using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_2025.DTOs.BookingDTO;
using WebAPI_2025.Enums;
using WebAPI_2025.Models.Wrappers;
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
            try
            {
                await _Service.Create(DTO);
                return Ok(new APIResponse<string>(true, "Booking created successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>(false, $"Error while creating booking: {ex.Message}"));
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var bookings = await _Service.GetAll();
                return Ok(new APIResponse<List<BookingDTO>>(true, "Bookings fetched successfully", bookings));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>(false, $"Error fetching bookings: {ex.Message}"));
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetbyID(int Id)
        {
            try
            {
                var booking = await _Service.GetById(Id);
                return booking == null
                    ? NotFound(new APIResponse<string>(false, "Booking not found"))
                    : Ok(new APIResponse<BookingDTO>(true, "Booking retrieved", booking));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>(false, $"Error retrieving booking: {ex.Message}"));
            }
        }

        [HttpGet("user/{UID}")]
        public async Task<IActionResult> GetbyUser(int UID)
        {
            try
            {
                var booking = await _Service.GetBookingByUserId(UID);
                return booking == null || booking.Count == 0
                    ? NotFound(new APIResponse<string>(false, "No bookings found for this user"))
                    : Ok(new APIResponse<List<BookingDTO>>(true, "User bookings retrieved", booking));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>(false, $"Error fetching user's bookings: {ex.Message}"));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BookingUpdateDTO booking)
        {
            if (booking == null || booking.BookingID != id)
                return BadRequest("Invalid booking data.");

            await _Service.Update(id, booking);
            return Ok(new APIResponse<string> { Success = true, Message = "Booking updated." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _Service.Delete(id);
                return Ok(new APIResponse<string>(true, "Booking deleted successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>(false, $"Error deleting booking: {ex.Message}"));
            }
        }
        [HttpPut("approve/{id}")]
        public async Task<IActionResult> Approve(int id)
        {
            var success = await _Service.UpdateStatus(id, BookingStatus.Approved);
            if (success) return Ok(new APIResponse<string>(true, "Booking approved"));
            return NotFound(new APIResponse<string>(false, "Booking not found"));
        }
        [HttpPut("reject/{id}")]
        public async Task<IActionResult> Reject(int id)
        {
            var success = await _Service.UpdateStatus(id, BookingStatus.Rejected);
            if (success) return Ok(new APIResponse<string>(true, "Booking rejected"));
            return NotFound(new APIResponse<string>(false, "Booking not found"));
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromQuery] BookingStatus status)
        {
            try
            {
                var success = await _Service.UpdateStatus(id, status);
                if (success)
                    return Ok(new APIResponse<string>(true, "Booking status updated"));
                return NotFound(new APIResponse<string>(false, "Booking not found"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>(false, $"Update failed: {ex.Message}"));
            }
        }
    }
}
