using Microsoft.AspNetCore.Mvc;
using Workshop.Infrastructure.Repositories;
using Workshop.DTOs;
using Workshop.Core.Interfaces;
using Workshop.Core.Entities;
using Workshop.Core.Services;

namespace Workshop.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _adminService.GetAllUsersAsync();
            return Ok(users);
        }

		[HttpPost("workshop")]
        public async Task<IActionResult> AddAutoRepairShop([FromBody] AutoRepairShopDto autoRepairShopDto)
        {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
                await _adminService.AddAutoRepairShopAsync(autoRepairShopDto.Email, autoRepairShopDto.Address, autoRepairShopDto.PhoneNumber);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}
    }
}
