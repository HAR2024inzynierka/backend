using Workshop.Core.Entities;


namespace Workshop.Core.Interfaces
{
	public interface IAdminService //pomenjat parametri metoda AddAutoRepairShopAsync na objekt AutoRepairShop + perenesti ego v AutoRepairShopService
	{
		Task<List<User>> GetAllUsersAsync();
		Task AddAutoRepairShopAsync(string email, string address, string phoneNumber);
		
	}
}
