using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
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
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        //Relatioonships
        public ICollection<Blog> Blogs { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Rate> Rates { get; set; }
    }
}