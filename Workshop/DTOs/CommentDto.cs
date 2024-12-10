namespace Workshop.DTOs
{
    public class CommentDto
    {
        public required string Content { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
}
