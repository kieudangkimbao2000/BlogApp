using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApp.Entities;

public class Rate
{
    [Key]
    public string Id { get; set; }
    [Required]
    public int Score { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    //Foreign Keys
    [ForeignKey("Author")]
    public string AuthorId { get; set; }
    [ForeignKey("BlogPost")]
    public string BlogId { get; set; }

    //Relationships
    public Account Author { get; set; }
    public Blog BlogPost { get; set; }
}
