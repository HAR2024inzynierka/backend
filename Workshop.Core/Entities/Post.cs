
namespace Workshop.Core.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int AutoRepairShopId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<Like> Likes { get; set; } = null!;
        public List<Comment> Comments { get; set; } = null!;
    }
}
