using TypeGen.Core.TypeAnnotations;

namespace BlogApp.DTOs;

[ExportTsInterface]
public class TagListRespDTO : RespDTO
{
    public  List<TagDTO> datas {get; set; }

    public TagListRespDTO(List<TagDTO> tags, int statCd, string  msg) : base(statCd, msg)
    {
        this.datas = tags;
    }
}