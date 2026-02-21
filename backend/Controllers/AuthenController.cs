namespace BlogApp.Controllers;

using BlogApp.DTOs;
using BlogApp.Interfaces;
using BlogApp.DTOs.Authentications;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Common;

[ApiController]
[Route("api/authen")]
public class AuthenController(IAuthenService authenService) : ControllerBase
{

    [HttpPost("login")]
    public ActionResult<AccountDTO?> LoginUser([FromBody] LoginDTO login)
    {
        string errCode = "";
        var token = authenService.LoginUser(login, ref errCode);

        if (token == "")
        {
            return BadRequest(AppMessages.GetMessage(errCode));
        }
        
        return Ok(token);
    }

    [HttpPost("register")]
    public ActionResult<AccountDTO?> Register([FromBody] RegisterDTO register)
    {
        string errCode = "";
        var accountDTO = authenService.RegisterUser(register, ref errCode);

        if (accountDTO == null)
        {
            return BadRequest(AppMessages.GetMessage(errCode));
        }

        return Ok(accountDTO);
    }
}