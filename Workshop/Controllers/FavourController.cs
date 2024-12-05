using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workshop.Core.Interfaces;
using Workshop.DTOs;
using Workshop.Core.Entities;

namespace Workshop.Controllers
{
    /// <summary>
    /// Kontroler do zarządzania usługami w warsztatach.
    /// </summary>
    [Authorize]
	[ApiController]
	[Route("api/admin/[controller]")]
	public class FavourController : ControllerBase
	{
		private readonly IFavourService _favourService;

        /// <summary>
        /// Konstruktor kontrolera, który inicjalizuje serwis do obsługi usług.
        /// </summary>
        /// <param name="favourService">Serwis odpowiedzialny za operacje na usługach.</param>
        public FavourController(IFavourService favourService)
		{
			_favourService = favourService;
		}

        /// <summary>
        /// Pobiera szczegóły usługi na podstawie identyfikatora.
        /// </summary>
        /// <param name="favourId">Identyfikator usługi.</param>
        /// <returns>Usługa lub 404 Not Found, jeśli nie znaleziono.</returns>
        [HttpGet("{favourId}")]
		public async Task<IActionResult> GetFavourByIdAsync(int favourId)
		{
            var favour = await _favourService.GetFavourByIdAsync(favourId);

            //Sprawdzamy czy usługa istnieje. 
            if (favour == null)
            {
                return NotFound();
            }

            return Ok(favour);
        }

        /// <summary>
        /// Dodaje nową usługę do bazy danych.
        /// </summary>
        /// <param name="favourDto">Dane nowej usługi do dodania.</param>
        /// <returns>Status wykonania operacji dodawania usługi.</returns>
		[HttpPost("add")]
		public async Task<IActionResult> AddFavour([FromBody] AddFavourDto favourDto)
		{
            // Sprawdzenie, czy dane wejściowe są poprawne.
            if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
                // Tworzenie obiektu usługi na podstawie danych DTO.
                var favour = new Favour
				{
					TypeName = favourDto.TypeName,
					Description = favourDto.Description,
					Price = favourDto.Price,
                    AutoRepairShopId = favourDto.AutoServiceId
				};

				await _favourService.AddFavourAsync(favour);
				return Ok("Favour added successfully.");
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

        /// <summary>
        /// Aktualizuje dane istniejącej usługi.
        /// </summary>
        /// <param name="favourId">Identyfikator usługi do zaktualizowania.</param>
        /// <param name="favourDto">Dane usługi do aktualizacji.</param>
        /// <returns>Status wykonania operacji aktualizacji usługi.</returns>
		[HttpPut("{favourId}")]
		public async Task<IActionResult> UpdateFavour(int favourId, [FromBody]AddFavourDto favourDto)
		{
            // Sprawdzenie poprawności danych wejściowych.
            if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

            try
            {
                // Tworzenie obiektu usługi na podstawie danych DTO oraz identyfikatora.
                var favour = new Favour
                {
					Id = favourId,
                    TypeName = favourDto.TypeName,
					Description = favourDto.Description,
					Price = favourDto.Price,
                };
                await _favourService.UpdateFavourAsync(favour);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Usuwa usługę na podstawie identyfikatora.
        /// </summary>
        /// <param name="favourId">Identyfikator usługi do usunięcia.</param>
        /// <returns>Status wykonania operacji usunięcia usługi.</returns>
		[HttpDelete("{favourId}")]
		public async Task<IActionResult> DeleteFavour(int favourId)
		{
            try
            {
                await _favourService.DeleteFavourAsync(favourId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
	}
}
