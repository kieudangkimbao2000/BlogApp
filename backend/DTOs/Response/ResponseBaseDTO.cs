namespace BlogApp.DTOs;

public class ResponseBaseDTO
{
    public int StatusCode {get; set;}
    public string Message { get; set;}

    public ResponseBaseDTO(int stat, string msg)
    {
        this.StatusCode = stat;
        this.Message = msg;
    }
}