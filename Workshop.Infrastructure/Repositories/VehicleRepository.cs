﻿using Microsoft.EntityFrameworkCore;
using Workshop.Core.Entities;
using Workshop.Infrastructure.Data;

namespace Workshop.Infrastructure.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly WorkshopDbContext _context;

        public VehicleRepository (WorkshopDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Vehicle vehicle)
        {
            await _context.Vehicles.AddAsync(vehicle);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> RegistrationNumberExistsAsync(string registrationNumber)
        {
            return await _context.Vehicles.AnyAsync(u => u.RegistrationNumber == registrationNumber);
        }

        public async Task<List<Vehicle>> GetAllVehiclesOfUserAsync(int userId)
        {
            return await _context.Vehicles.Where(v => v.UserId == userId).ToListAsync();
        }
    }
}