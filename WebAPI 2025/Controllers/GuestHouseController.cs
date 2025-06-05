using Microsoft.AspNetCore.Mvc;
using WebAPI_2025.Repositories;
using WebAPI_2025.Services;
using WebAPI_2025.DTOs.GuestHouseDTO;
using WebAPI_2025.Models.Wrappers;

namespace WebAPI_2025.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestHouseController : Controller
    {
        private readonly IGuestHouseService _Service;
        public GuestHouseController(IGuestHouseService GHS)
        {
            _Service = GHS;
        }
        //POST: api/guesthouse
        [HttpPost]
        public async Task<IActionResult> Create(AddGuestHouseDTO dto)
        {
            try
            {
                await _Service.CreateAsync(dto);
                return Ok(new APIResponse<AddGuestHouseDTO>(true, "Guest House created successfully", dto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>(false, $"Creation failed: {ex.Message}"));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var guesthouses = await _Service.GetAllAsync();
                return Ok(new APIResponse<List<GetGuestHouseDTO>>(true, "Guest Houses retrieved", guesthouses));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>(false, $"Error fetching list: {ex.Message}"));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _Service.GetAsync(id);
                if (result == null)
                    return NotFound(new APIResponse<string>(false, "Guest house not found"));

                return Ok(new APIResponse<GetGuestHouseDTO>(true, "Guest House found", result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>(false, $"Error: {ex.Message}"));
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            try
            {
                _Service.Delete(id);
                return Ok(new APIResponse<string>(true, "Guest House deleted successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>(false, $"Delete failed: {ex.Message}"));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGuestHouse(UpdateGuestHouseDTO dto)
        {
            try
            {
                await _Service.UpdateGuestHouseAsync(dto);
                return Ok(new APIResponse<string>(true, "Guest House updated successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>(false, $"Update failed: {ex.Message}"));
            }
        }

        [HttpGet("locations")]
        public async Task<IActionResult> GetLocations()
        {
            try
            {
                var locations = await _Service.GetAllLocationsAsync();
                return Ok(new APIResponse<List<string>>(true, "Locations fetched successfully", locations));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>(false, $"Failed to fetch locations: {ex.Message}"));
            }
        }

        [HttpGet("by-location/{location}")]
        public async Task<IActionResult> GetGuestHousesByLocation(string location)
        {
            try
            {
                var result = await _Service.GetGuestHousesByLocationAsync(location);

                if (result == null || result.Count == 0)
                {
                    return NotFound(new APIResponse<string>(false, $"No guest houses found at location: {location}"));
                }

                return Ok(new APIResponse<List<GetGuestHouseDTO>>(true, "Guest houses retrieved successfully", result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>(false, $"Error retrieving guest houses: {ex.Message}"));
            }
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableGuestHouses([FromQuery] string location, [FromQuery] DateTime checkIn, [FromQuery] DateTime checkOut)
        {
            try
            {
                var result = await _Service.GetAvailableGuestHousesByLocationAsync(location, checkIn, checkOut);

                if (result == null || result.Count == 0)
                {
                    return NotFound(new APIResponse<string>(false, $"No available guest houses found at {location} for selected dates"));
                }

                return Ok(new APIResponse<List<GetGuestHouseDTO>>(true, "Available guest houses retrieved", result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>(false, $"Error retrieving available guest houses: {ex.Message}"));
            }
        }


    }
}
