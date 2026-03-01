namespace BlogApp.DTOs;

public class BlogLikeDTO    
{
    public string AuthorId { get; set; }
    public string BlogId { get; set; }
    public bool LikeOrDislike { get; set; }
}