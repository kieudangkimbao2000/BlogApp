using TypeGen.Core.TypeAnnotations;

namespace BlogApp.DTOs;

 [ExportTsInterface]
public class CommentLikeDTO
{
    public string AuthorId { get; set; }
    public string CommentId { get; set; }
    public bool LikeOrDislike { get; set; }
}