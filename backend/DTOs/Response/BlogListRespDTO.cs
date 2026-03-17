using TypeGen.Core.TypeAnnotations;

namespace BlogApp.DTOs;

[ExportTsInterface]
public class BlogListRespDTO : RespDTO
{
    public List<BlogDTO> datas { get; set; }

    public BlogListRespDTO(List<BlogDTO> blogs, int statCd, string msg) : base(statCd, msg)
    {
        this.datas = blogs;
    }
}