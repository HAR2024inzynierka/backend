using Microsoft.EntityFrameworkCore;
using Workshop.Core.Entities;
using Workshop.Core.Interfaces;
using Workshop.Infrastructure.Data;

namespace Workshop.Infrastructure.Repositories
{
    /// <summary>
    /// Implementacja repozytorium dla komentarzy.
    /// Odpowiada za operacje na encji Comment w bazie danych.
    /// </summary>
    public class CommentRepository : ICommentRepository
    {
        private readonly WorkshopDbContext _context;

        /// <summary>
        /// Konstruktor repozytorium komentarzy.
        /// Inicjalizuje kontekst bazy danych.
        /// </summary>
        /// <param name="context">Kontekst bazy danych WorkshopDbContext.</param>
        public CommentRepository(WorkshopDbContext context)
        {
            _context = context;
        }

        public async Task<Comment?> GetCommentByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<List<Comment>> GetAllCommentsByPostIdAsync(int postId)
        {
            return await _context.Comments
                .Include(c => c.User)
                .Where(c => c.PostId == postId) // Filtruje komentarze po PostId
                .ToListAsync();
        }

        public async Task AddCommentAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCommentAsync(Comment comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(Comment comment)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }
    }
}
