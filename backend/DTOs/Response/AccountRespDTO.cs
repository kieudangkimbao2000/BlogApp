namespace BlogApp.DTOs;

public class AccountRespDTO : ResponseBaseDTO
{
    public AccountDTO account {get; set;}

    public AccountRespDTO(AccountDTO acc, int stat, string msg) : base(stat, msg)
    {
        this.account = acc;
    }
}