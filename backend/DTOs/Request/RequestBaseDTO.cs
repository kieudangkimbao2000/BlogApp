namespace BlogApp.DTOs;

public class RequestBaseDTO
{
    public object datas { get; set; }
    public IFormFile[]? files {get; set;}
}