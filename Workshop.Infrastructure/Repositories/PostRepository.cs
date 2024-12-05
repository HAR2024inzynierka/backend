
using Microsoft.EntityFrameworkCore;
using Workshop.Core.Entities;
using Workshop.Core.Interfaces;
using Workshop.Infrastructure.Data;

namespace Workshop.Infrastructure.Repositories
{
    /// <summary>
    /// Implementacja repozytorium dla postów.
    /// Reprezentuje operacje na encji Post w bazie danych.
    /// </summary>
    public class PostRepository : IPostRepository
    {
        private readonly WorkshopDbContext _context;

        /// <summary>
        /// Konstruktor repozytorium postów.
        /// Inicjalizuje repozytorium z kontekstem bazy danych.
        /// </summary>
        /// <param name="context">Kontekst bazy danych WorkshopDbContext.</param>
        public PostRepository(WorkshopDbContext context)
        {
            _context = context;
        }

        public async Task<Post?> GetPostByIdAsync(int id)
        {
           return await _context.Posts
                .Include(p => p.Comments) // Załączamy komentarze do posta
                .Include(p => p.Likes) // Załączamy polubienia do posta
                .FirstOrDefaultAsync(p => p.Id == id); // Wyszukujemy post po jego ID
        }

        public async Task AddPostAsync(Post post)
        {
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePostAsync(Post post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(Post post)
        {
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Post>> GetAllPostsAsync()
        {
            return await _context.Posts
                .Include(p => p.Comments) // Załączamy komentarze do posta
                .Include(p => p.Likes) // Załączamy polubienia do posta
                .ToListAsync();
        }

        public async Task<List<Post>> GetPostsByAutoServiceIdAsync(int autoRepairShopId)
        {
            return await _context.Posts
                .Where(p => p.AutoRepairShopId == autoRepairShopId) // Filtrowanie postów po ID warsztatu
                .Include(p => p.Comments) // Załączamy komentarze do posta
                .Include(p => p.Likes) // Załączamy polubienia do posta
                .ToListAsync();
                
        }
    }
}
