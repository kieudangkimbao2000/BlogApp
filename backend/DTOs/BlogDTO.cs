using System.ComponentModel;

namespace BlogApp.DTOs
{
    public class BlogDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Categories { get; set; }
        public int State { get; set; }
        public int AmountOfAccesses { get; set; }
        public DateTime? PublishedAt { get; set; }
        public bool? LikedOrDislikedByUser { get; set; }
        public string AuthorId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}