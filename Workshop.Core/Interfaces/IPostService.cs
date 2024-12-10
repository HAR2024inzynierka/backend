using Workshop.Core.Entities;

namespace Workshop.Core.Interfaces
{
    public interface IPostService
    {
        //Metody dla postów
        Task<Post> GetPostByIdAsync(int id);
        Task<List<Post>> GetAllPostsAsync();
        Task<List<Post>> GetPostsByAutoServiceIdAsync(int autoRepairShopId);
        Task AddPostAsync(Post post);
        Task UpdatePostAsync(Post post);
        Task DeletePostAsync(int postId);

        //Metody dla komentarzy
        Task<List<Comment>> GetCommentsByPostIdAsync(int postId);
        Task AddCommentAsync(Comment commnet);
        Task UpdateCommentAsync(Comment commnet);
        Task DeleteCommentAsync(int commnetId);

        //Metod dla polubień
        Task<int> GetLikeCountByPostIdAsync(int postId);
        Task<bool> IsUserLikedPostAsync(int userId, int postId);
        Task AddLikeAsync(int userId, int postId);
        Task RemoveLikeAsync(int userId, int postId);

    }
}
