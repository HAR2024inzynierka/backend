namespace Workshop.DTOs
{
    public class PostDto
    {
        public required string Title { get; set; }
        public required string Content { get; set; }
        public int AutoRepairShopId { get; set; }
    }
}
