namespace BlogApp.DTOs;

public class ReportDTO
{
    public string Id { get; set; }
    public string TargetType { get; set; }
    public string TargetId { get; set; }
    public string Content { get; set; }
    public string ReporterId { get; set; }
    public DateTime? CreatedAt { get; set; }
}