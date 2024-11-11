using Microsoft.AspNetCore.Mvc;
using Workshop.Core.Entities;
using Workshop.Core.Interfaces;
using Workshop.DTOs;

namespace Workshop.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AutoRepairShopController : ControllerBase
	{
		private readonly IAutoRepairShopService _autoRepairShopService;
		private readonly ITermService _termService;
		private readonly IFavourService _favourService;
		private readonly IRecordService _recordService;
		public AutoRepairShopController(IAutoRepairShopService autoRepairShopService, ITermService termService, IFavourService favourService, IRecordService recordService) 
		{
			_autoRepairShopService = autoRepairShopService;
			_termService = termService;
			_favourService = favourService;
			_recordService = recordService;
		}

		[HttpGet("workshops")]
		public async Task<IActionResult> GetAllAutoRepairShops()
		{
			var autoRepairShops = await _autoRepairShopService.GetAllAutoRepairShopsAsync();
			return Ok(autoRepairShops);
		}

		[HttpGet("{autoServiceId}/terms")]
		public async Task<IActionResult> GetTermsByAutoServiceId(int autoServiceId)
		{
			var terms = await _termService.GetTermsByAutoServiceIdAsync(autoServiceId);
			return Ok(terms);
		}

		[HttpGet("{autoServiceId}/favours")]
		public async Task<IActionResult> GetFavoursByAutoServiceId(int autoServiceId)
		{
			var fovours = await _favourService.GetFavoursByAutoServiceIdAsync(autoServiceId);
			return Ok(fovours);
		}

		[HttpPost("add-record")]
		public async Task<IActionResult> AddRecord([FromBody] AddRecordDto recordDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				var record = new Record
				{
					VehicleId = recordDto.VehicleId,
					FavourId = recordDto.FavourId,
					TermId = recordDto.TermId,
					RecordDate = recordDto.RecordDate,
					CompletionDate = recordDto.CompletionDate
				};

				await _recordService.AddRecordAsync(record);
				return Ok("Record added successfully.");
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}
	}
}
