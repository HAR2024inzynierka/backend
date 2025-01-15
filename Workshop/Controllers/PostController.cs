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
        private readonly ITokenService _tokenService;

        public PostController(IPostService postService, ITokenService tokenService)
        {
            _postService = postService;
            _tokenService = tokenService;
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
                    UserId = _tokenService.GetUserIdFromToken(HttpContext)
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
            var showCommentDtos = comments.Select(c => new ShowCommentDto
            {
                Id = c.Id,
                Content = c.Content,
                PostId = c.PostId,
                UserId = c.UserId,
                Username = c.User.Login
            });
            return Ok(showCommentDtos);
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
                    UserId = _tokenService.GetUserIdFromToken(HttpContext),
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
        public async Task<IActionResult> IsPostLikedByUser(int id)
        {
            var userId = _tokenService.GetUserIdFromToken(HttpContext);
            var isLiked = await _postService.IsUserLikedPostAsync(userId, id);
            return Ok(isLiked);
        }

        [Authorize]
        [HttpPost("{id}/like")]
        public async Task<IActionResult> AddLike(int id)
        {
            var userId = _tokenService.GetUserIdFromToken(HttpContext);
            await _postService.AddLikeAsync(userId, id);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}/like")]
        public async Task<IActionResult> RemoveLike(int id)
        {
            var userId = _tokenService.GetUserIdFromToken(HttpContext);
            await _postService.RemoveLikeAsync(userId, id);
            return Ok();
        }
    }
}
