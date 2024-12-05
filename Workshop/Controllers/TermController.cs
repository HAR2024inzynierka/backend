using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workshop.Core.Entities;
using Workshop.Core.Interfaces;
using Workshop.DTOs;

namespace Workshop.Controllers
{
    /// <summary>
    /// Kontroler odpowiedzialny za zarządzanie terminami w warsztacie.
    /// </summary>
	[Authorize]
	[ApiController]
	[Route("api/admin/[controller]")]
	public class TermController : ControllerBase
	{
		private readonly ITermService _termService;

        /// <summary>
        /// Konstruktor kontrolera, który inicjalizuje serwis terminów.
        /// </summary>
        /// <param name="termService">Serwis odpowiedzialny za operacje na terminach.</param>
        public TermController(ITermService termService)
		{
			_termService = termService;
		}

        /// <summary>
        /// Pobiera szczegóły terminu na podstawie jego identyfikatora.
        /// </summary>
        /// <param name="termId">Identyfikator terminu.</param>
        /// <returns>Jeśli termin istnieje, zwraca jego dane. W przeciwnym razie zwraca błąd 404.</returns>
        [HttpGet("{termId}")]
		public async Task<IActionResult> GetTermById(int termId)
		{
            var term = await _termService.GetTermByIdAsync(termId);

            //Sprawdzamy czy termin istnieje. 
            if (term == null)
            {
                return NotFound();
            }

            return Ok(term);
        }

        /// <summary>
        /// Dodaje nowy termin do systemu.
        /// </summary>
        /// <param name="termDto">Obiekt DTO zawierający dane nowego terminu.</param>
        /// <returns>Status operacji (sukces lub błąd).</returns>
		[HttpPost("add")]
		public async Task<IActionResult> AddTerm([FromBody] AddTermDto termDto)
		{
            // Sprawdzanie poprawności danych wejściowych
            if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
                // Tworzenie nowego obiektu Term z danych DTO
                var term = new Term
				{
					StartDate = termDto.StartDate,
					EndDate = termDto.EndDate,
					Availability = termDto.Availability,
					AutoServiceId = termDto.AutoServiceId
				};

				await _termService.AddTermAsync(term);
				return Ok("Term added successfully.");
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

        /// <summary>
        /// Aktualizuje istniejący termin.
        /// </summary>
        /// <param name="termId">Identyfikator terminu, który ma zostać zaktualizowany.</param>
        /// <param name="termDto">Nowe dane terminu.</param>
        /// <returns>Status operacji (sukces lub błąd).</returns>
        [HttpPut("{termId}")]
        public async Task<IActionResult> UpdateTerm(int termId, [FromBody] AddTermDto termDto)
        {
            // Sprawdzanie poprawności danych wejściowych
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Tworzenie obiektu Term z nowymi danymi
                var term = new Term
                {
                    Id = termId,
                    StartDate = termDto.StartDate,
                    EndDate = termDto.EndDate,
                    Availability = termDto.Availability,
                    AutoServiceId = termDto.AutoServiceId

                };
                await _termService.UpdateTermAsync(term);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Usuwa termin na podstawie jego identyfikatora.
        /// </summary>
        /// <param name="termId">Identyfikator terminu do usunięcia.</param>
        /// <returns>Status operacji (sukces lub błąd).</returns>
        [HttpDelete("{termId}")]
        public async Task<IActionResult> DeleteTerm(int termId)
        {
            try
            {
                await _termService.DeleteTermAsync(termId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
