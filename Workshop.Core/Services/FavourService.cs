using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop.Core.Entities;
using Workshop.Core.Interfaces;
using Workshop.Infrastructure.Repositories;

namespace Workshop.Core.Services
{
	public class FavourService: IFavourService
	{
		private readonly IFavourRepository _favourRepository;

		public FavourService(IFavourRepository favourRepository)
		{
			_favourRepository = favourRepository;
		}

		public async Task<Favour> GetFavourByIdAsync(int id)
		{
			return await _favourRepository.GetFavourByIdAsync(id);
		}

		public async Task AddFavourAsync(Favour favour)
		{
			await _favourRepository.AddFavourAsync(favour);
		}

		public async Task UpdateFavourAsync(Favour updateFavour)
		{
            var favour = await _favourRepository.GetFavourByIdAsync(updateFavour.Id);
            if (favour == null)
            {
                throw new Exception("Favour not found");
            }

            favour.TypeName = updateFavour.TypeName;
			favour.Description = updateFavour.Description;
			favour.Price = updateFavour.Price;
            
			await _favourRepository.UpdateFavourAsync(favour);
        }

		public async Task DeleteFavourAsync(int id)
		{
            var favour = await _favourRepository.GetFavourByIdAsync(id);
            if (favour == null)
            {
                throw new Exception("Favour not found");
            }
			await _favourRepository.DeleteFavourAsync(favour);
        }

		public async Task<List<Favour>> GetFavoursByAutoServiceIdAsync(int autoserviceId)
		{
			return await _favourRepository.GetFavoursByAutoServiceIdAsync(autoserviceId);
		}
	}
}
