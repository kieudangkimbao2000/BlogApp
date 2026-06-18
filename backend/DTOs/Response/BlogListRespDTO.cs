namespace BlogApp.DTOs;

public class BlogListRespDTO : ResponseBaseDTO
{
    public List<BlogDTO> datas { get; set; }

    public BlogListRespDTO(List<BlogDTO> blogs, int statCd, string msg) : base(statCd, msg)
    {
        this.datas = blogs;
    }
}