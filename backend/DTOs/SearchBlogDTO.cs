using TypeGen.Core.TypeAnnotations;

namespace  BlogApp.DTOs;

 [ExportTsInterface]
public class SearchBlogDTO()
{
    private string SearchTitle { get; set; }
    private string SearchFlag {get; set; }
    private string[] SearchCategories { get; set; }
    private string SearchPage { get; set; }
}