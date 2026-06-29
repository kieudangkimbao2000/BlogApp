namespace BlogApp.Controllers;

using BlogApp.DTOs;
using BlogApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Common;
using Azure;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/authen")]
public class AuthenController(IAuthenService authenService) : ControllerBase
{

    [HttpPost("login")]
    public ActionResult<LoginRespDTO> LoginUser([FromForm] LoginReqDTO login)
    {
        ResponseBaseDTO resp = authenService.LoginUser(login);

        if (resp.StatusCode == 400)
        {   
            return BadRequest(resp);
        }
        
        return Ok(resp);
    }

    [HttpPost("register")]
    
    public ActionResult<ResponseBaseDTO> Register([FromBody] RegisterDTO register)
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

    [HttpGet("check-valid-token")]
    [Authorize]
    public ActionResult<ResponseBaseDTO> CheckValidToken()
    {
        return Ok(new ResponseBaseDTO(200, "Token is valid"));
    }
}