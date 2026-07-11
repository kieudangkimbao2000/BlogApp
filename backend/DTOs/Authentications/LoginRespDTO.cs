namespace BlogApp.DTOs;

public class LoginRespDTO
{
    public string Token { get; set; }
    public LoginRespDTO(string token)
    {
        this.Token = token;
    }
}