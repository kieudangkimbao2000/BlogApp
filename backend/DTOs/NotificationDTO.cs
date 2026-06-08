namespace BlogApp.DTOs;
public class NotificationDTO
{
    public string Content { get; set; }
    public string TargetType { get; set; }
    public string TargetId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string ReceiverId { get; set; }
}