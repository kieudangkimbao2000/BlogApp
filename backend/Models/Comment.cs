using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlogApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Models;

public class Comment
{
    [Key]
    public string Id { get; set; }
    [Required]
    public string Content { get; set; }
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    public string State { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    //Foreign Keys
    [ForeignKey("Author")]
    public string AuthorId { get; set; }
    [ForeignKey("BlogPost")]
    public string BlogId { get; set; }
    [ForeignKey("Parent")]
    public string ParentId { get; set; }

    //Relationships
    public Account Author { get; set; }
    public Blog BlogPost { get; set; }
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Comment Parent{ get; set; }
}
