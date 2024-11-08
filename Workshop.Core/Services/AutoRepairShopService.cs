using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop.Core.Entities;
using Workshop.Core.Interfaces;

namespace Workshop.Core.Services
{
	public class AutoRepairShopService : IAutoRepairShopService
	{
		private readonly IAutoRepairShopRepository _autoRepairShopRepository;

		public AutoRepairShopService(IAutoRepairShopRepository autoRepairShopRepository)
		{
			_autoRepairShopRepository = autoRepairShopRepository;
		}

		public async Task<List<AutoRepairShop>> GetAllAutoRepairShopsAsync()
		{
			return await _autoRepairShopRepository.GetAllAutoRepairShopsAsync();
		}
	}
}
