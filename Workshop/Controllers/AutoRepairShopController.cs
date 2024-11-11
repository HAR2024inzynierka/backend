using Microsoft.AspNetCore.Mvc;
using Workshop.Core.Interfaces;

namespace Workshop.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AutoRepairShopController : ControllerBase
	{
		private readonly IAutoRepairShopService _autoRepairShopService;
		private readonly ITermService _termService;
		private readonly IFavourService _favourService;
		public AutoRepairShopController(IAutoRepairShopService autoRepairShopService, ITermService termService, IFavourService favourService) 
		{
			_autoRepairShopService = autoRepairShopService;
			_termService = termService;
			_favourService = favourService;
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
	}
}
