namespace BlogApp.Controllers;

using BlogApp.DTOs;
using BlogApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/authen")]
[Authorize]
public class AccountController(IAccountService service, IAccountValidator validator) : ControllerBase
{
    [HttpPost("upload-avatar")]
    public IActionResult UploadAvatar(RequestBaseDTO<UploadAvatarReqDTO> req)
    {
        if(!validator.ValidateUploadAvatarReq(req))
        {
            return BadRequest();
        }

        var resp = service.UploadAvatar(req.Datas.Username, req.Base64Strings[0]);

        return StatusCode(resp.StatusCode, resp);
    }
}