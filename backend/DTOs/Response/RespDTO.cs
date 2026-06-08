namespace BlogApp.DTOs;

public class RespDTO
{
    public int StatusCode {get; set;}
    public string Message { get; set;}

    public RespDTO(int stat, string msg)
    {
        this.StatusCode = stat;
        this.Message = msg;
    }
}