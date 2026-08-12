namespace BlogApp.DTOs;

public class ChangePasswordReqDTO
{
    public string Email { get; set; }
    public string RePassword { get; set; }
    public string NewPassword { get; set; }
}