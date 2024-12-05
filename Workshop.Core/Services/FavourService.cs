using Workshop.Core.Entities;
using Workshop.Core.Interfaces;

namespace Workshop.Core.Services
{
    /// <summary>
    /// Serwis odpowiedzialny za operacje związane z usługami oferowanymi przez warsztaty.
    /// Zawiera metody umożliwiające zarządzanie usługami
	/// </summary>
    public class FavourService: IFavourService
	{
		private readonly IFavourRepository _favourRepository;

        /// <summary>
        /// Konstruktor serwisu FavourService.
        /// </summary>
        /// <param name="favourRepository">Repozytorium operujące na danych usług</param>
        public FavourService(IFavourRepository favourRepository)
		{
			_favourRepository = favourRepository;
		}

		public async Task<Favour> GetFavourByIdAsync(int id)
		{
			var favour = await _favourRepository.GetFavourByIdAsync(id);

            // Jeśli usługa nie istnieje, rzuca wyjątek
            if (favour == null)
			{
				throw new Exception("Favour not found");
			}

            return favour;
		}

		public async Task AddFavourAsync(Favour favour)
		{
			await _favourRepository.AddFavourAsync(favour);
		}

		public async Task UpdateFavourAsync(Favour updateFavour)
		{
            var favour = await _favourRepository.GetFavourByIdAsync(updateFavour.Id);

            // Jeśli usługa nie istnieje, rzuca wyjątek
            if (favour == null)
            {
                throw new Exception("Favour not found");
            }

            // Aktualizuje właściwości przysługi
            favour.TypeName = updateFavour.TypeName;
			favour.Description = updateFavour.Description;
			favour.Price = updateFavour.Price;
            
			await _favourRepository.UpdateFavourAsync(favour);
        }

		public async Task DeleteFavourAsync(int id)
		{
            var favour = await _favourRepository.GetFavourByIdAsync(id);

            // Jeśli usługa nie istnieje, rzuca wyjątek
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
