namespace BlogApp.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

public class Comment
{
    [Key]
    public string Id { get; set; }
    [Required]
    public string Content { get; set; }
    [MaxLength(1)]
    [Required]
    public int State { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    //Foreign Keys
    [ForeignKey("Author")]
    public string AuthorId { get; set; }
    [ForeignKey("BlogPost")]
    public string BlogId { get; set; }
    [ForeignKey("Parent")]
    public string? ParentId { get; set; }

    //Relationships
    public Account Author { get; set; }
    public Blog BlogPost { get; set; }
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Comment Parent{ get; set; }
    public ICollection<Comment> Replies { get; set; }
    public ICollection<CommentLike> Likes { get; set; }
}
