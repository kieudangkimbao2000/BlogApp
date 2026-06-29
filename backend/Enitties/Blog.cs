namespace BlogApp.Entities;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Blog
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public string CoverImage {get; set;}
        [Required]
        public string[] Tags { get; set; }
        [DefaultValue(0)]
        public int AmountOfAccesses { get; set; }
        [Required]
        [MaxLength(1)]
        public int State { get; set; }
        public DateTime? PublishedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        //Foreign Keys
        [ForeignKey("Author")]
        public string AuthorId { get; set; }

        //Relationships
        public Account Author { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<BlogLike> Likes { get; set; }
    }