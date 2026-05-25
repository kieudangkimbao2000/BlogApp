namespace BlogApp.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Report
{
    [Key]
    public string Id { get; set; }
    [Required]
    public string TargetType { get; set; }
    [Required]
    public string TargetId { get; set; }
    [Required]
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    //Foreign Keys
    [ForeignKey("Reporter")]
    public string ReporterId { get; set; }

    //Relationships
    public Account Reporter { get; set; }
}