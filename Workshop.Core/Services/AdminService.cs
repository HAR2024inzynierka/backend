﻿using Workshop.Core.Entities;
using Workshop.Core.Interfaces;
using Workshop.Infrastructure.Repositories;

namespace Workshop.Core.Services
{
	public class AdminService : IAdminService
	{
		private readonly IAutoRepairShopRepository _autoRepairShopRepository;
		private readonly IUserRepository _userRepository;
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
