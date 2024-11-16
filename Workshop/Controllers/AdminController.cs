using Microsoft.AspNetCore.Mvc;
using Workshop.DTOs;
using Workshop.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Workshop.Core.Entities;
using Workshop.Core.Services;

namespace Workshop.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IAutoRepairShopService _autoRepairShopService;

        public AdminController(IAdminService adminService, IAutoRepairShopService autoRepairShopService)
        {

            _adminService = adminService;
            _autoRepairShopService = autoRepairShopService;
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

        [HttpPut("{autoServiceId}")]
        public async Task<IActionResult> UpdateAutoRepairShop(int autoServiceId, [FromBody] AutoRepairShopDto autoServiceDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var autoRepairShop = new AutoRepairShop
                {
                    Id = autoServiceId,
                    Email = autoServiceDto.Email,
                    Address = autoServiceDto.Address,
                    PhoneNumber = autoServiceDto.PhoneNumber
                };

                await _autoRepairShopService.UpdateAsync(autoRepairShop);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{autoServiceId}")]
        public async Task<IActionResult> DeleteAutoRepairShop(int autoServiceId)
        {
            try
            {
                await _autoRepairShopService.DeleteAsync(autoServiceId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
