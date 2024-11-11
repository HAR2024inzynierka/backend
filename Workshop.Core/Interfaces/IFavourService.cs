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
		Task AddFavourAsync(Favour favour);
		Task<List<Favour>> GetFavoursByAutoServiceIdAsync(int autoserviceId);
	}
}
