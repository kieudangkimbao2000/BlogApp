namespace BlogApp.Controllers;

using BlogApp.DTOs;
using BlogApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Common;
using Microsoft.Build.Tasks;
using Azure;

[ApiController]
[Route("api/authen")]
public class AuthenController(IAuthenService authenService) : ControllerBase
{

    [HttpPost("login")]
    public ActionResult<LoginRespDTO> LoginUser([FromBody] LoginDTO login)
    {
        RespDTO resp = authenService.LoginUser(login);

        if (resp.StatusCode == 400)
        {   
            return BadRequest(resp);
        }
        
        return Ok(resp);
    }

    [HttpPost("register")]
    public ActionResult<RespDTO> Register([FromBody] RegisterDTO register)
    {
        var resp = authenService.RegisterUser(register);

        switch(resp.StatusCode)
        {
            case 400:
                return BadRequest(resp);
            case 500:
                return StatusCode(StatusCodes.Status500InternalServerError, resp);
            default:
                break;
        }

        return Ok(resp);
    }
}