namespace BlogApp.DTOs;

public class ResponseBaseDTO
{
    public int StatusCode {get; set;}
    public ResponseBaseDTO(int stat)
    {
        this.StatusCode = stat;

    }
}

public class ResponseBaseDTO<T> : ResponseBaseDTO
{
    public T Datas { get; set; }

    public ResponseBaseDTO(T datas, int stat) : base(stat)
    {
        this.Datas = datas;
    }
}   