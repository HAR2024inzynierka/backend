using Workshop.Core.Entities;
using Workshop.Core.Interfaces;
using Workshop.Infrastructure.Repositories;

namespace Workshop.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ILikeRepository _likeRepository;

        public PostService(IPostRepository postRepository, ICommentRepository commentRepository, ILikeRepository likeRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _likeRepository = likeRepository;
        }

        //Metody dla postów
        public async Task<Post> GetPostByIdAsync(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            
            if(post == null)
            {
                throw new Exception("Post not found");
            }

            return post;
        }

        public async Task<List<Post>> GetAllPostsAsync()
        {
            return await _postRepository.GetAllPostsAsync();
        }

        public async Task<List<Post>> GetPostsByAutoServiceIdAsync(int autoRepairShopId)
        {
            return await _postRepository.GetPostsByAutoServiceIdAsync(autoRepairShopId);
        }

        public async Task AddPostAsync(Post post)
        {
            await _postRepository.AddPostAsync(post);
        }

        public async Task UpdatePostAsync(Post post)
        {
            var existingPost = await _postRepository.GetPostByIdAsync(post.Id);

            if(existingPost == null)
            {
                throw new KeyNotFoundException("Post not found");
            }

            existingPost.Title = post.Title;
            existingPost.Content = post.Content;
            existingPost.AutoRepairShopId = post.AutoRepairShopId;

            await _postRepository.UpdatePostAsync(existingPost);
        }

        public async Task DeletePostAsync(int postId)
        {
            var post = await _postRepository.GetPostByIdAsync(postId);

            if(post == null)
            {
                throw new KeyNotFoundException("Post not found");
            }

            await _postRepository.DeletePostAsync(post);
        }

        //Metody dla komentarzy
        public async Task<List<Comment>> GetCommentsByPostIdAsync(int postId)
        {
            return await _commentRepository.GetAllCommentsByPostIdAsync(postId);
        }

        public async Task AddCommentAsync(Comment commnet)
        {
            await _commentRepository.AddCommentAsync(commnet);
        }

        public async Task UpdateCommentAsync(Comment commnet)
        {
            var existingComment = await _commentRepository.GetCommentByIdAsync(commnet.Id);

            if(existingComment == null)
            {
                throw new KeyNotFoundException("Comment not found");
            }

            existingComment.Content = commnet.Content;

            await _commentRepository.UpdateCommentAsync(existingComment);
        }

        public async Task DeleteCommentAsync(int commnetId)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(commnetId);

            if(comment == null)
            {
                throw new KeyNotFoundException("Commnet not found");
            }

            await _commentRepository.DeleteCommentAsync(comment);
        }

        //Metod dla polubień
        public async Task<int> GetLikeCountByPostIdAsync(int postId)
        {
            return await _likeRepository.GetLikeCountByPostIdAsync(postId);
        }

        public async Task<bool> IsUserLikedPostAsync(int userId, int postId)
        {
            return await _likeRepository.IsUserLikedPostAsync(userId, postId);
        }

        public async Task AddLikeAsync(int userId, int postId)
        {
            var post = await _postRepository.GetPostByIdAsync(postId);

            if(post == null)
            {
                throw new KeyNotFoundException("Post not found");
            }

            var alreadyLiked = await _likeRepository.IsUserLikedPostAsync(userId, postId);

            if (alreadyLiked)
            {
                throw new InvalidOperationException("User already liked this post");
            }

            var like = new Like { PostId = postId, UserId = userId };

            await _likeRepository.AddLikeAsync(like);
        }

        public async Task RemoveLikeAsync(int userId, int postId)
        {
            var like = await _likeRepository.GetLikeByUserIdAndPostIdAsync(userId, postId);

            if(like == null)
            {
                throw new KeyNotFoundException("Like not found for the given user and post");
            }

            await _likeRepository.DeleteLikeAsync(like);
        }
    }
}
