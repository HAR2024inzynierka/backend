using Microsoft.AspNetCore.Mvc;
using Workshop.Core.Interfaces;
using System.Threading.Tasks;
using Workshop.Core.Entities;

namespace Workshop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost("{userId}/vehicles")]
        public async Task<IActionResult> AddVehicles(int userId, [FromBody] Vehicle vehicle)
        {
            if (vehicle == null)
            {
                return BadRequest();
            }

            vehicle.UserId = userId;
            await _userService.AddVehicleAsync(vehicle);
            return CreatedAtAction(nameof(AddVehicles), new { id = vehicle.Id }, vehicle);
        }
    }
}
