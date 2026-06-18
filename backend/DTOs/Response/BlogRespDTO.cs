namespace BlogApp.DTOs;

public class BlogRespDTO : ResponseBaseDTO
{
    public BlogRespDTO(BlogDTO? blg, int stat, string msg) : base(stat, msg)
    {
        this.blog = blg;
    }

    public BlogDTO? blog { get; set; }
}