namespace BlogApp.DTOs
{
    public class AccountDTO
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string OtherContact { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        public string Role { get; set; }
        public string State { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}