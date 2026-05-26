using TypeGen.Core.TypeAnnotations;

namespace BlogApp.DTOs
{
     [ExportTsInterface]
    public class CommentDTO
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public int State { get; set; }
        public bool? LikedByUser { get; set; }
        public string AuthorId { get; set; }
        public string BlogId { get; set; }
        public string? ParentId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}