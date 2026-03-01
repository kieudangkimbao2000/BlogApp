namespace BlogApp.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class CommentLike
{
    //Keys
    [Key]
    [ForeignKey("Author")]
    public string AuthorId { get; set; }
    [Key]
    [ForeignKey("Comment")]
    public string CommentId { get; set; }
    [Required]
    public bool LikeOrDislike { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    //Relationships
    public Account Author { get; set; }
    public Comment Comment { get; set; }
}