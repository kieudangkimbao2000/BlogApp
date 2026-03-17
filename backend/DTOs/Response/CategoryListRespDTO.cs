using TypeGen.Core.TypeAnnotations;

namespace BlogApp.DTOs;

[ExportTsInterface]
public class CategoryListRespDTO : RespDTO
{
    public  List<CategoryDTO> datas {get; set; }

    public CategoryListRespDTO(List<CategoryDTO> categs, int statCd, string  msg) : base(statCd, msg)
    {
        this.datas = categs;
    }
}