using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models;

public class Category
{
    [Key]
    public string Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
