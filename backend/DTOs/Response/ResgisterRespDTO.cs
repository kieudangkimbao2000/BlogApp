namespace BlogApp.DTOs;

using TypeGen.Core.TypeAnnotations;

[ExportTsInterface]
public class RegisterRespDTO : RespDTO
{
    public AccountDTO account {get; set;}

    public RegisterRespDTO(AccountDTO acc, int stat, string msg) : base(stat, msg)
    {
        this.account = acc;
    }
}