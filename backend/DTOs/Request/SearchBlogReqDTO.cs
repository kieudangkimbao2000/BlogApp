using TypeGen.Core.TypeAnnotations;

namespace SearchBlogReqDTO;

[ExportTsInterface]
public class SearchBlogReqDTO
{
    public  string SearchTitle {get; set; }
    public string[] Categories { get; set;}
    //0 : New, 1: Top
    public int SearchFlag { get; set;} 
}