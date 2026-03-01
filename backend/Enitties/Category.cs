namespace BlogApp.Entities;

using System.ComponentModel.DataAnnotations;

public class Category
{
    [Key]
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
