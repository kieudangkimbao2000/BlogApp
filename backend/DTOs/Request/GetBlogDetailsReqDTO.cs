namespace BlogApp.DTOs;

public class GetBlogDetailsReqDTO
{
    public string BlogId { get; set; }
    public string? UserId { get; set; }
}