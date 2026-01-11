using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApp.Models
{
    public class Blog
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public string Categories { get; set; }
        [Column(TypeName = "decimal(1,1)")]
        public decimal Rating { get; set; }
        public int AmountOfAccesses { get; set; }
        public int AmountOfComments { get; set; }
        public int AmountOfRates { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        //Foreign Keys
        [ForeignKey("Author")]
        public string AuthorId { get; set; }

        //Relationships
        public Account Author { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Rate> Rates { get; set; }
    }
}