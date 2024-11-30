using Workshop.Core.Entities;

namespace Workshop.Core.Interfaces
{
	public interface IFavourRepository
	{
		Task<Favour?> GetFavourByIdAsync(int id);
		Task AddFavourAsync(Favour favour);
		Task UpdateFavourAsync(Favour favour);
		Task DeleteFavourAsync(Favour favour);
		Task<List<Favour>> GetFavoursByAutoServiceIdAsync(int autoserviceId);
	}
}
