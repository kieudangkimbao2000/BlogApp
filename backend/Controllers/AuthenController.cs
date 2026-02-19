namespace BlogApp.Controllers;

using BlogApp.DTOs;
using BlogApp.Interfaces;
using BlogApp.DTOs.Authentications;
using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/authen")]
    public class AuthenController(IAuthenService authenService) : ControllerBase
    {

        [HttpPost("login")]
        public ActionResult<AccountDTO?> LoginUser([FromBody] LoginDTO login)
        {
            var (token, errMessage) = authenService.LoginUser(login);
            if (token == "")
            {
                return BadRequest(errMessage);
            }
            return Ok(token);
        }

        [HttpPost("register")]
        public ActionResult<AccountDTO?> Register([FromBody] RegisterDTO register)
        {
            var (accountDTO, errMessage) = authenService.RegisterUser(register);
            if (accountDTO == null)
            {
                return BadRequest(errMessage);
            }
            return Ok(accountDTO);
        }
    }