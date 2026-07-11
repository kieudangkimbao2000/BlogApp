namespace BlogApp.DTOs;

public class ErrorRespDTO
{
    public string Message { get; set; }
    public ErrorRespDTO(string message)
    {
        this.Message = message;
    }
}