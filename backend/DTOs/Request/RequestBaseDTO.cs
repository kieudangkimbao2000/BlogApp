namespace BlogApp.DTOs;

public class RequestBaseDTO
{
    public string[]? Base64Strings { get; set; }
}

public class RequestBaseDTO<T> : RequestBaseDTO
{
    public T Datas { get; set; } = default!;
}