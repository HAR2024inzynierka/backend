using Workshop.Core.Entities;


namespace Workshop.Core.Interfaces
{
	public interface IAdminService
	{
		Task<List<User>> GetAllUsersAsync();
		Task<List<AutoRepairShop>> GetAllAutoRepairShopsAsync();
		Task AddAutoRepairShopAsync(string email, string address, string phoneNumber);
		
	}
}
