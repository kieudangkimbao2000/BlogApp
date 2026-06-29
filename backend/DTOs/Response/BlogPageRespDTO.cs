using Azure;

namespace BlogApp.DTOs;

public class BlogPageRespDTO : ResponseBaseDTO
{
    public PageDTO<BlogDTO> page { get; set; }

    public BlogPageRespDTO(PageDTO<BlogDTO> p, int statCd, string msg) : base(statCd, msg)
    {
        this.page = p;
    }
}