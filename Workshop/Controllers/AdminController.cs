using Microsoft.AspNetCore.Mvc;
using Workshop.DTOs;
using Workshop.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Workshop.Core.Entities;

namespace Workshop.Controllers
{
    /// <summary>
    /// Kontroler admina odpowiedzialny za zarządzanie aplikacją.
    /// Wymaga autoryzacji użytkownika.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IAutoRepairShopService _autoRepairShopService;

        /// <summary>
        /// Konstruktor kontrolera, który inicjalizuje zależności serwisów.
        /// </summary>
        /// <param name="adminService">Serwis do zarządzania użytkownikami.</param>
        /// <param name="autoRepairShopService">Serwis do zarządzania warsztatami.</param>
        public AdminController(IAdminService adminService, IAutoRepairShopService autoRepairShopService)
        {

            _adminService = adminService;
            _autoRepairShopService = autoRepairShopService;
        }

        /// <summary>
        /// Pobiera wszystkich użytkowników z systemu.
        /// </summary>
        /// <returns>Lista użytkowników.</returns>
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _adminService.GetAllUsersAsync();
            return Ok(users);
        }

        /// <summary>
        /// Dodaje nowy warsztat do systemu.
        /// </summary>
        /// <param name="autoRepairShopDto">Dane warsztatu, który ma zostać dodany.</param>
        /// <returns>Status operacji.</returns>
		[HttpPost("workshop")]
        public async Task<IActionResult> AddAutoRepairShop([FromBody] AutoRepairShopDto autoRepairShopDto)
        {
            // Sprawdzamy, czy dane wejściowe są poprawne
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

        /// <summary>
        /// Aktualizuje dane istniejącego warsztatu.
        /// </summary>
        /// <param name="autoServiceId">Identyfikator warsztatu, który ma zostać zaktualizowany.</param>
        /// <param name="autoServiceDto">Dane warsztatu do zaktualizowania.</param>
        /// <returns>Status operacji.</returns>
        [HttpPut("{autoServiceId}")]
        public async Task<IActionResult> UpdateAutoRepairShop(int autoServiceId, [FromBody] AutoRepairShopDto autoServiceDto)
        {
            // Sprawdzamy, czy dane wejściowe są poprawne
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Przygotowujemy obiekt AutoRepairShop z danymi do zaktualizowania.
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

        /// <summary>
        /// Usuwa warsztat z systemu na podstawie jego identyfikatora.
        /// </summary>
        /// <param name="autoServiceId">Identyfikator warsztatu do usunięcia.</param>
        /// <returns>Status operacji.</returns>
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
