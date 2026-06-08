namespace BlogApp.DTOs;
public class PageDTO<T>
{
    public List<T> Datas { get; set; }
    public int CurPage {get; set;}
    public int PageSize { get; set; }
}