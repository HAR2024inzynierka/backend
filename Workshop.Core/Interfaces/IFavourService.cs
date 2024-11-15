using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop.Core.Entities;

namespace Workshop.Core.Interfaces
{
	public interface IFavourService
	{
		Task<Favour> GetFavourByIdAsync(int id);
		Task AddFavourAsync(Favour favour);
		Task UpdateFavourAsync(Favour favour);
		Task DeleteFavourAsync(int id);
		Task<List<Favour>> GetFavoursByAutoServiceIdAsync(int autoserviceId);
	}
}
