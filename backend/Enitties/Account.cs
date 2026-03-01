namespace BlogApp.Entities;

using System.ComponentModel.DataAnnotations;

public class Account
{
    [Key]
    public string Username { get; set; }
    [Required]
    public byte[] Password { get; set; }
    [Required]
    public string Name {get; set; }
    public string Address { get; set; }
    [Phone]
    public string Phone { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public string OtherContact{ get; set; }
    public string Description { get; set; }
    public string Avatar { get; set; }
    [MaxLength(1)]
    public string Role { get; set; }
    [MaxLength(1)]
    public string State { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    //Relatioonships
    public ICollection<Blog> Blogs { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public ICollection<BlogLike> BlogLikes { get; set; }
    public ICollection<CommentLike> CommentLikes { get; set; }
}