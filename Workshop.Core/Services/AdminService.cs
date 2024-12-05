using Workshop.Core.Entities;
using Workshop.Core.Interfaces;
using Workshop.Infrastructure.Repositories;

namespace Workshop.Core.Services
{
    /// <summary>
    /// Serwis administracyjny, który realizuje operacje zarządzania użytkownikami
    /// oraz warsztatami samochodowymi.
    /// </summary>
    public class AdminService : IAdminService
	{
		private readonly IAutoRepairShopRepository _autoRepairShopRepository;
		private readonly IUserRepository _userRepository;

        /// <summary>
        /// Konstruktor, który inicjalizuje serwis administracyjny.
        /// </summary>
        /// <param name="autoRepairShopRepository">Repozytorium do operacji na warsztatach samochodowych.</param>
        /// <param name="userRepository">Repozytorium do operacji na użytkownikach.</param>
        public AdminService(IAutoRepairShopRepository autoRepairShopRepository, IUserRepository userRepository) 
		{ 
			_autoRepairShopRepository = autoRepairShopRepository;
			_userRepository = userRepository;
		}

		public async Task<List<User>> GetAllUsersAsync()
		{
			return await _userRepository.GetAllUsersAsync();
		}

		public async Task AddAutoRepairShopAsync(string email, string address, string phoneNumber)
		{
			var autoRepairShop = new AutoRepairShop
			{
				Email = email,
				Address = address,
				PhoneNumber = phoneNumber
			};
			await _autoRepairShopRepository.AddAsync(autoRepairShop);
		}
	}
}
