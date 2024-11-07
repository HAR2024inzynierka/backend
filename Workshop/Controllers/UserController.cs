using Microsoft.AspNetCore.Mvc;
using Workshop.Core.Interfaces;
using System.Threading.Tasks;
using Workshop.Core.Entities;
using Workshop.DTOs;
using Microsoft.AspNetCore.Authorization;
using Workshop.Filters;

namespace Workshop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [ServiceFilter(typeof(AuthorizeUserFilter))]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("{userId}/vehicles")]
        public async Task<IActionResult> GetAllVehicles(int userId)
        {
            var vehicles = await _userService.GetAllVehiclesAsync(userId);
            return Ok(vehicles);
        }

        [HttpPost("{userId}/vehicle")]
        public async Task<IActionResult> AddVehicle(int userId, [FromBody] VehicleDto vehicleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _userService.AddVehicleAsync(userId, vehicleDto.Brand, vehicleDto.Model, vehicleDto.RegistrationNumber);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
