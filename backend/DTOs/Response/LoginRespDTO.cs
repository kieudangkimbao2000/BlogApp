namespace BlogApp.DTOs;

public class LoginRespDTO : RespDTO
{
    public string JWT {get; set;}

    public LoginRespDTO(string jwt, int stat, string msg) : base(stat, msg)
    {
        this.JWT = jwt;
    }
}