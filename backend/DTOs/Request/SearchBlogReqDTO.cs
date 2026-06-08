namespace BlogApp.DTOs;

public class SearchBlogReqDTO
{
    public  string SearchTitle {get; set; }
    public string[] Categories { get; set;}
    //0 : New, 1: Top
    public int SearchFlag { get; set;} 
    public int CurPage { get; set; }
}