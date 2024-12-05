using Workshop.Core.Entities;


namespace Workshop.Core.Interfaces
{
    /// <summary>
    /// Interfejs serwisu administracyjnego, który udostępnia metody zarządzania użytkownikami
    /// i warsztatami samochodowymi.
    /// </summary>
    public interface IAdminService //pomenjat parametri metoda AddAutoRepairShopAsync na objekt AutoRepairShop + perenesti ego v AutoRepairShopService
	{
        /// <summary>
        /// Pobiera listę wszystkich użytkowników.
        /// </summary>
        /// <returns>Lista wszystkich użytkowników w systemie.</returns>
		Task<List<User>> GetAllUsersAsync();

        /// <summary>
        /// Dodaje nowy warsztat samochodowy do systemu.
        /// </summary>
        /// <param name="email">Adres e-mail warsztatu.</param>
        /// <param name="address">Adres warsztatu samochodowego.</param>
        /// <param name="phoneNumber">Numer telefonu warsztatu samochodowego.</param>
		Task AddAutoRepairShopAsync(string email, string address, string phoneNumber);
		
	}
}
