﻿using Microsoft.EntityFrameworkCore;
using Workshop.Core.Entities;
using Workshop.Infrastructure.Data;

namespace Workshop.Infrastructure.Repositories
{
    /// <summary>
    /// Repozytorium do operacji na pojazdach.
    /// Implementuje metody dostępu do danych związanych z pojazdami w bazie danych.
    /// </summary>
    public class VehicleRepository : IVehicleRepository
    {
        private readonly WorkshopDbContext _context;

        /// <summary>
        /// Konstruktor, który inicjalizuje repozytorium z kontekstem bazy danych.
        /// </summary>
        /// <param name="context">Kontekst bazy danych</param>
        public VehicleRepository (WorkshopDbContext context)
        {
            _context = context;
        }

        public async Task<Vehicle?> GetVehicleByIdAsync(int id)
        {
            return await _context.Vehicles.FindAsync(id);
        }

        public async Task AddAsync(Vehicle vehicle)
        {
            await _context.Vehicles.AddAsync(vehicle);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Vehicle vehicle)
        {
            _context.Vehicles.Update(vehicle);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Vehicle vehicle)
        {
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> VINExistsAsync(string VIN)
        {
            return await _context.Vehicles.AnyAsync(u => u.VIN == VIN);
        }

        public async Task<List<Vehicle>> GetAllVehiclesOfUserAsync(int userId)
        {
            return await _context.Vehicles
                .Where(v => v.UserId == userId) // Filtrujemy pojazdy po użytkowniku
                .ToListAsync();
        }
    }
}
