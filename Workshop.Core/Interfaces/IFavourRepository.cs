using Workshop.Core.Entities;

namespace Workshop.Core.Interfaces
{
	public interface IFavourRepository
	{
		Task AddFavourAsync(Favour favour);
		Task<List<Favour>> GetFavoursByAutoServiceIdAsync(int autoserviceId);
	}
}
