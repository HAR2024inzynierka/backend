using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop.Core.Entities;
using Workshop.Core.Interfaces;

namespace Workshop.Core.Services
{
	public class FavourService: IFavourService
	{
		private readonly IFavourRepository _favourRepository;

		public FavourService(IFavourRepository favourRepository)
		{
			_favourRepository = favourRepository;
		}

		public async Task AddFavourAsync(Favour favour)
		{
			await _favourRepository.AddFavourAsync(favour);
		}
		public async Task<List<Favour>> GetFavoursByAutoServiceIdAsync(int autoserviceId)
		{
			return await _favourRepository.GetFavoursByAutoServiceIdAsync(autoserviceId);
		}
	}
}
