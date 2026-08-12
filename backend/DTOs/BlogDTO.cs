using System.ComponentModel;
using BlogApp.Common;

namespace BlogApp.DTOs
{
    public class BlogDTO
    {
        public string? Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string? CoverImage { get; set;}
        public string[] Tags { get; set; }
        public int? AmountOfAccesses { get; set; }
        public BlogState State { get; set; }
        public DateTime? PublishedAt { get; set; }
        public bool? LikedByUser { get; set; }
        public string AuthorId { get; set; }
        public string? AuthorName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}