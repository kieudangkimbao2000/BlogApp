namespace BlogApp.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class BlogLike
{
    //Keys
    [ForeignKey("Author")]
    public string AuthorId { get; set; }
    [ForeignKey("BlogPost")]
    public string BlogId { get; set; }
    [Required]
    public bool LikeOrDislike { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    //Relationships
    public Account Author { get; set; }
    public Blog BlogPost { get; set; }
}
