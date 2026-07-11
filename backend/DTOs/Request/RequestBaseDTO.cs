namespace BlogApp.DTOs;

public class RequestBaseDTO<T>
{
    public T Datas { get; set; } = default!;
    public string[]? Base64Strings { get; set; }
}