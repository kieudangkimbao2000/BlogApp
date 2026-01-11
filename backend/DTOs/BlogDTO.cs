namespace BlogApp.DTOs
{
    public class BlogDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Categories { get; set; }
        public decimal Rating { get; set; }
        public int AmountOfAccesses { get; set; }
        public int AmountOfComments { get; set; }
        public int AmountOfRates { get; set; }
        public string AuthorId { get; set; }
    }
}