namespace BlogApp.Controllers;

using BlogApp.DTOs;
using BlogApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

/// <summary>
///   Authentication Controller
/// </summary>
/// <param name="authenService"></param>
[ApiController]
[Route("api/authen")]
public class AuthenController(IAuthenService authenService, 
                                IAuthenValidator validator) : ControllerBase
{

    /// <summary>
    ///     Route for user login
    /// </summary>
    /// <param name="req">Login request</param>
    /// <returns>Token / Error Message</returns>
    [HttpPost("login")]
    public IActionResult LoginUser([FromBody] RequestBaseDTO<LoginReqDTO> req)
    {
        if(!validator.ValidateLoginRequest(req.Datas))
        {
            return BadRequest();
        }

        var resp = authenService.LoginUser(req.Datas);

        return StatusCode(resp.StatusCode, resp);    
    }

    [HttpPost("register")]
    
    /// <summary>
    ///    Route for user registration
    /// </summary>
    /// <param name="req">Registration request</param>
    /// <returns>Account Info / Error Message</returns>
    public IActionResult Register([FromForm] RequestBaseDTO<RegisterDTO> req)
    {
        var resp = authenService.RegisterUser(req.Datas);

        return StatusCode(resp.StatusCode, resp);
    }

    /// <summary>
    ///   Route for checking the validity of the token
    /// </summary>
    /// <returns>Status Code</returns>
    [HttpGet("check-valid-token")]
    [Authorize]
    public IActionResult CheckValidToken()
    {
        return StatusCode(200, new ResponseBaseDTO(200));
    }
}