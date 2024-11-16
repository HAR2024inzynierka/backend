using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop.Core.Entities;

namespace Workshop.Core.Interfaces
{
	public interface IAutoRepairShopService
	{
		Task<AutoRepairShop> GetAutoRepairShopByIdAsync(int id);
		Task UpdateAsync(AutoRepairShop autoRepairShop);
		Task DeleteAsync(int id);
		Task<List<AutoRepairShop>> GetAllAutoRepairShopsAsync();

	}
}
