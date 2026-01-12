namespace BlogApp.Controllers;
using BlogApp.DTOs;
using BlogApp.DIs;
using BlogApp.Models;
using BlogApp.Models.Authentications;
using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public LoginController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("authenticate")]
        public ActionResult<AccountDTO?> Authenticate([FromBody] Login login)
        {
            var (accountDTO, errMessage) = _authenticationService.AuthenticateUser(login);
            if (accountDTO == null)
            {
                return BadRequest(errMessage);
            }
            return Ok(accountDTO);
        }

        [HttpPost("register")]
        public ActionResult<AccountDTO?> Register([FromBody] Register register)
        {
            var (accountDTO, errMessage) = _authenticationService.RegisterUser(register);
            if (accountDTO == null)
            {
                return BadRequest(errMessage);
            }
            return Ok(accountDTO);
        }
    }