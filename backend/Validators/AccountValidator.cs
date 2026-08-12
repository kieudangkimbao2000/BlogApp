using BlogApp.DTOs;
using BlogApp.Interfaces;

namespace BlogApp.Validators;

public class AccountValidator : IAccountValidator
{
    public bool ValidateUploadAvatarReq(RequestBaseDTO<UploadAvatarReqDTO> req)
    {
        if(req == null || req.Datas == null || string.IsNullOrEmpty(req.Datas.Username) || 
                                req.Base64Strings == null || req.Base64Strings.Count() == 0)
        {
            return false;
        }

        return true;
    }
}