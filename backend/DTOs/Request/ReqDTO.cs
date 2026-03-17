using TypeGen.Core.TypeAnnotations;

namespace BlogApp.DTOs;

[ExportTsInterface]
public class ReqDTO
{
    public int page {get; set;}
}