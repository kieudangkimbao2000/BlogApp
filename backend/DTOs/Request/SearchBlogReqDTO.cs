namespace BlogApp.DTOs;

public class SearchBlogReqDTO : RequestBaseDTO
{
    public  string SearchTitle {get; set; }
    public string[] Tags { get; set;}
    //0 : New, 1: Top
    public int SearchFlag { get; set;} 
    public int CurPage { get; set; }
}