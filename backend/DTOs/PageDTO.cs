using TypeGen.Core.TypeAnnotations;

namespace BlogApp.DTOs;

[ExportTsInterface]
public class PageDTO<T>
{
    public List<T> Datas { get; set; }
    public int CurPage {get; set;}
    public int PageSize { get; set; }
}