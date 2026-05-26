using TypeGen.Core.TypeAnnotations;

namespace BlogApp.DTOs;

 [ExportTsInterface]
public class BlogLikeDTO    
{
    public string AuthorId { get; set; }
    public string BlogId { get; set; }
    public bool LikeOrNot { get; set; }
}