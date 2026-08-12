namespace BlogApp.Controllers;

using BlogApp.DTOs;
using BlogApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;

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
    public IActionResult Register([FromBody] RequestBaseDTO<RegisterReqDTO> req)
    {
        var resp = authenService.RegisterUser(req.Datas);

        return StatusCode(resp.StatusCode, resp);
    }

    [HttpPost("validate-register-info")]
    public IActionResult ValidateRegisterInfo([FromBody] RequestBaseDTO<RegisterReqDTO> req)
    {
        if(!validator.ValidateRegisterRequest(req.Datas))
        {
            return BadRequest();
        }

        var resp = authenService.VerifyRegisterInfo(req.Datas);

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

    /// <summary>
    ///  Route for email authentication
    /// </summary>
    /// <param name="req">Email authentication request</param>
    /// <returns>OTP Hash / Error Message</returns>
    [HttpPost("authenticate-email")]
    public IActionResult AuthenticateEmail([FromBody] RequestBaseDTO<AuthenEmailReqDTO> req)
    {
        var resp = authenService.AuthenticateEmail(req.Datas);

        return StatusCode(resp.StatusCode, resp);
    }

    /// <summary>
    ///     Route for verifying email OTP
    /// </summary>
    /// <param name="req">Request for checking the email OTP</param>
    /// <returns>True if successful, otherwise false</returns>
    [HttpPost("verify-email-otp")]
    public IActionResult VerifyOTPEmail([FromBody] RequestBaseDTO<VerifyEmailOTPReqDTO> req)
    {
        var resp = authenService.VerifyEmailOTP(req.Datas);

        return StatusCode(resp.StatusCode, resp);
    }

    /// <summary>
    ///    Route for changing password
    /// </summary>
    /// <param name="req">Change password request</param>
    /// <returns>Success or error message</returns>
    [HttpPost("change-password")]
    public IActionResult ChangePassword([FromBody] RequestBaseDTO<ChangePasswordReqDTO> req)
    {
        if(!validator.ValidateChangePasswordRequest(req.Datas))
        {
            return BadRequest();
        }

        var resp = authenService.ChangePassword(req.Datas);

        return StatusCode(resp.StatusCode, resp);
    }
}