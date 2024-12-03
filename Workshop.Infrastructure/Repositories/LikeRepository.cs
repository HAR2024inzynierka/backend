using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using Workshop.Core.Entities;
using Workshop.Core.Interfaces;
using Workshop.Infrastructure.Data;

namespace Workshop.Infrastructure.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly WorkshopDbContext _context;

        public LikeRepository(WorkshopDbContext context)
        {
            _context = context;
        }

        public async Task<Like?> GetLikeByIdAsync(int id)
        {
            return await _context.Likes.FindAsync(id);
        }

        public async Task AddLikeAsync(Like like)
        {
            await _context.Likes.AddAsync(like);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLikeAsync(Like like)
        {
            _context.Likes.Remove(like);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetLikeCountByPostIdAsync(int postsId)
        {
            return await _context.Likes.CountAsync(l => l.PostId == postsId);
        }

        public async Task<bool> IsUserLikedPostAsync(int userId, int postId)
        {
            return await _context.Likes.AnyAsync(l => l.UserId == userId && l.PostId == postId);
        }
    }
}
