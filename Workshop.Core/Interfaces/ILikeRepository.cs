using Workshop.Core.Entities;

namespace Workshop.Core.Interfaces
{
    public interface ILikeRepository
    {
        Task<Like?> GetLikeByIdAsync(int id);
        Task AddLikeAsync(Like like);
        Task DeleteLikeAsync(Like like);
        Task<int> GetLikeCountByPostIdAsync(int postsId);
        Task<bool> IsUserLikedPostAsync(int userId, int postId);
        
    }
}
