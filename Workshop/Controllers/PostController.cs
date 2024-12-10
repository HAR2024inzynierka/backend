using Microsoft.AspNetCore.Mvc;
using Workshop.Core.Interfaces;
using Workshop.DTOs;
using Workshop.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.Design;

namespace Workshop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            return Ok(post);
        }
        [HttpGet("posts")]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postService.GetAllPostsAsync();
            return Ok(posts);
        }

        [Authorize]
        [HttpPost("{id}/comment")]
        public async Task<IActionResult> AddComment(int id, [FromBody] CommentDto commentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var comment = new Comment
                {
                    Content = commentDto.Content,
                    PostId = id,
                    UserId = commentDto.UserId
                };

                await _postService.AddCommentAsync(comment);
                return Ok("Comment added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpGet("{id}/comments")]
        public async Task<IActionResult> GetCommentsForPost(int id)
        {
            var comments = await _postService.GetCommentsByPostIdAsync(id);
            return Ok(comments);
        }

        [Authorize]
        [HttpPut("{postId}/comment/{commentId}")]
        public async Task<IActionResult> UpdateComment(int commentId, [FromBody] CommentDto commentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var comment = new Comment
                {
                    Id = commentId,
                    Content = commentDto.Content,
                    UserId = commentDto.UserId,
                    PostId = commentDto.PostId
                };

                await _postService.UpdateCommentAsync(comment);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("{id}/comment/{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            await _postService.DeleteCommentAsync(commentId);
            return Ok();
        }

        [HttpGet("{id}/likeCount")]
        public async Task<IActionResult> GetLikeCountForPost(int id)
        {
            var likeCount = await _postService.GetLikeCountByPostIdAsync(id);
            return Ok(likeCount);
        }

        [Authorize]
        [HttpGet("{id}/isLiked")]
        public async Task<IActionResult> IsPostLikedByUser(int id, [FromQuery] int userId)
        {
            var isLiked = await _postService.IsUserLikedPostAsync(userId, id);
            return Ok(isLiked);
        }

        [Authorize]
        [HttpPost("{id}/like")]
        public async Task<IActionResult> AddLike(int id, [FromBody] LikeDto likeDto)
        {
            await _postService.AddLikeAsync(id, likeDto.UserId);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}/like")]
        public async Task<IActionResult> RemoveLike(int id, [FromBody] LikeDto likeDto)
        {
            await _postService.RemoveLikeAsync(id, likeDto.UserId);
            return Ok();
        }
    }
}
