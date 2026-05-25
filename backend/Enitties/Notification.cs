namespace BlogApp.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Notification
{
    [Key]
    public string Id { get; set; }
    [Required]
    public string Content { get; set; }
    [Required]
    public string TargetType { get; set; }
    [Required]
    public string TargetId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    //Foreign Keys
    [ForeignKey("Receiver")]
    public string ReceiverId { get; set; }

    //Relationships
    public Account Receiver { get; set; }
}