namespace BlogApp.DTOs;

public class SearchBlogRespDTO : RespDTO
{
    public PageDTO<BlogDTO> blogs {get; set; }

    public SearchBlogRespDTO(PageDTO<BlogDTO> blogs, int statCd, string msg) : base(statCd, msg)
    {
        this.blogs = blogs;
    }
}