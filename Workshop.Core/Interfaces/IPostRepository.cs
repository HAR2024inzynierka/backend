using Workshop.Core.Entities;

namespace Workshop.Core.Interfaces
{
    public interface IPostRepository
    {
        Task<Post?> GetPostByIdAsync(int id);
        Task AddPostAsync(Post post);
        Task UpdatePostAsync(Post post);
        Task DeletePostAsync(Post post);
        Task<List<Post>> GetAllPostsAsync();
        Task<List<Post>> GetPostsByAutoServiceIdAsync(int autoRepairShopId);
    }
}
