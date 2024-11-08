using Microsoft.AspNetCore.Mvc;
using Workshop.Core.Interfaces;

namespace Workshop.Controllers
{
	public class AutoRepairShopController : ControllerBase
	{
		private readonly IAutoRepairShopService _autoRepairShopService;
		public AutoRepairShopController(IAutoRepairShopService autoRepairShopService) 
		{
			_autoRepairShopService = autoRepairShopService;
		}

		[HttpGet("workshops")]
		public async Task<IActionResult> GetAllAutoRepairShops()
		{
			var autoRepairShops = await _autoRepairShopService.GetAllAutoRepairShopsAsync();
			return Ok(autoRepairShops);
		}
	}
}
