namespace BlogApp.DTOs
{
    public class CommentDTO
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public string State { get; set; }
        public string AuthorId { get; set; }
        public string BlogId { get; set; }
        public string ParentId { get; set; }
    }
}