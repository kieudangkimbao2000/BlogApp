namespace BlogApp.DTOs;
public class TagListRespDTO : RespDTO
{
    public  List<TagDTO> datas {get; set; }

    public TagListRespDTO(List<TagDTO> tags, int statCd, string  msg) : base(statCd, msg)
    {
        this.datas = tags;
    }
}