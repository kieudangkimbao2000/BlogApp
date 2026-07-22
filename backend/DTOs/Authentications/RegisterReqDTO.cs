namespace BlogApp.DTOs;
    
public class RegisterReqDTO
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string FullName {get; set; }
    public string Email { get; set; }
}