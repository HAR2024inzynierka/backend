using Microsoft.EntityFrameworkCore;
using Workshop.Core.Entities;
using Workshop.Infrastructure.Data;

namespace Workshop.Infrastructure.Repositories
{
    /// <summary>
    /// Repozytorium do zarządzania użytkownikami w systemie warsztatów samochodowych.
    /// Implementuje operacje na danych związanych z użytkownikami.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly WorkshopDbContext _context;

        /// <summary>
        /// Konstruktor repozytorium, który przyjmuje kontekst bazy danych.
        /// </summary>
        /// <param name="context">Kontekst bazy danych</param>
        public UserRepository(WorkshopDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
