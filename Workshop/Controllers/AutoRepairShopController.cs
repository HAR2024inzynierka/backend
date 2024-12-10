using Microsoft.AspNetCore.Mvc;
using Workshop.Core.Entities;
using Workshop.Core.Interfaces;
using Workshop.DTOs;

namespace Workshop.Controllers
{
    /// <summary>
    /// Kierownik odpowiedzialny za warsztaty samochodowe i wszystko, co z nimi związanую
    /// </summary>
    [ApiController]
	[Route("api/[controller]")]
	public class AutoRepairShopController : ControllerBase
	{
		private readonly IAutoRepairShopService _autoRepairShopService;
		private readonly ITermService _termService;
		private readonly IFavourService _favourService;
		private readonly IRecordService _recordService;
        private readonly IPostService _postService;

        /// <summary>
        /// Konstruktor kontrolera, który inicjalizuje zależności serwisów.
        /// </summary>
        /// <param name="autoRepairShopService">Serwis odpowiedzialny za operacje na warsztatach.</param>
        /// <param name="termService">Serwis odpowiedzialny za operacje na terminach.</param>
        /// <param name="favourService">Serwis odpowiedzialny za operacje na usługach.</param>
        /// <param name="recordService">Serwis odpowiedzialny za operacje na rekordach.</param>
        /// /// <param name="postService">Serwis odpowiedzialny za operacje na postach.</param>
        public AutoRepairShopController(IAutoRepairShopService autoRepairShopService, ITermService termService, IFavourService favourService, IRecordService recordService, IPostService postService) 
		{
			_autoRepairShopService = autoRepairShopService;
			_termService = termService;
			_favourService = favourService;
			_recordService = recordService;
            _postService = postService;
		}

        /// <summary>
        /// Pobiera warsztat na podstawie identyfikatora.
        /// </summary>
        /// <param name="autoServiceId">Identyfikator warsztatu.</param>
        /// <returns>Wynik warsztatu lub 404 Not Found, jeśli nie znaleziono.</returns>
        [HttpGet("{autoServiceId}")]
        public async Task<IActionResult> GetAutoRepairShopById(int autoServiceId)
        {
            var autoRepairShop = await _autoRepairShopService.GetAutoRepairShopByIdAsync(autoServiceId);

            //Sprawdzamy czy warsztat istnieje.
            if (autoRepairShop == null)
            {
                return NotFound();
            }

            return Ok(autoRepairShop);
        }

        /// <summary>
        /// Pobiera wszystkie dostępne warsztaty.
        /// </summary>
        /// <returns>Lista wszystkich warsztatów.</returns>
        [HttpGet("workshops")]
		public async Task<IActionResult> GetAllAutoRepairShops()
		{
			var autoRepairShops = await _autoRepairShopService.GetAllAutoRepairShopsAsync();
			return Ok(autoRepairShops);
		}

        /// <summary>
        /// Pobiera terminy związane z danym warsztatem.
        /// </summary>
        /// <param name="autoServiceId">Identyfikator warsztatu.</param>
        /// <returns>Lista terminów dla danego warsztatu.</returns>
        [HttpGet("{autoServiceId}/terms")]
		public async Task<IActionResult> GetTermsByAutoServiceId(int autoServiceId)
		{
			var terms = await _termService.GetTermsByAutoServiceIdAsync(autoServiceId);
			return Ok(terms);
		}

        /// <summary>
        /// Pobiera usługi dostępne w danym warsztacie.
        /// </summary>
        /// <param name="autoServiceId">Identyfikator warsztatu.</param>
        /// <returns>Lista usług dostępnych w warsztacie.</returns>
        [HttpGet("{autoServiceId}/favours")]
		public async Task<IActionResult> GetFavoursByAutoServiceId(int autoServiceId)
		{
			var fovours = await _favourService.GetFavoursByAutoServiceIdAsync(autoServiceId);
			return Ok(fovours);
		}

        /// <summary>
        /// Dodaje rekord dotyczący naprawy w danym warsztacie.
        /// </summary>
        /// <param name="recordDto">Dane rekordu do dodania.</param>
        /// <returns>Status wykonania operacji.</returns>
        [HttpPost("add-record")]
		public async Task<IActionResult> AddRecord([FromBody] AddRecordDto recordDto)
		{
            // Sprawdzamy poprawność danych wejściowych
            if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
                // Tworzymy nowy obiekt rekordu na podstawie danych z DTO.
                var record = new Record
				{
					VehicleId = recordDto.VehicleId,
					FavourId = recordDto.FavourId,
					TermId = recordDto.TermId,
                    RecordDate = DateTime.Now
                };

				await _recordService.AddRecordAsync(record);
				return Ok("Record added successfully.");
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

        [HttpGet("{autoRepairShopId}/posts")]
        public async Task<IActionResult> GetPostsByAutoRepairShopId(int autoRepairShopId)
        {
            var posts = await _postService.GetPostsByAutoServiceIdAsync(autoRepairShopId);
            return Ok(posts);
        }
    }
}
