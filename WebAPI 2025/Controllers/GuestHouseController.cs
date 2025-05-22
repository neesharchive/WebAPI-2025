using Microsoft.AspNetCore.Mvc;
using WebAPI_2025.Repositories;
using WebAPI_2025.Services;
using WebAPI_2025.DTOs.GuestHouseDTO;

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
        public async Task<IActionResult> Create(AddGuestHouseDTO addGuestHouseDTO)
        {
            await _Service.CreateAsync(addGuestHouseDTO);
            return Ok(addGuestHouseDTO);
        }
        //GET: api/guesthouse
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var guesthouses=await _Service.GetAllAsync();
            return Ok(guesthouses);
        }
        //GET: api/guesthouse/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            
            var result= await _Service.GetAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        //Delete: api/guesthouse
        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            _Service.Delete(id);
            return NoContent();
        }

    }
}
