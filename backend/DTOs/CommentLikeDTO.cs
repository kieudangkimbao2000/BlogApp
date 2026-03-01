namespace BlogApp.DTOs;

public class CommentLikeDTO
{
    public string AuthorId { get; set; }
    public string CommentId { get; set; }
    public bool LikeOrDislike { get; set; }
}