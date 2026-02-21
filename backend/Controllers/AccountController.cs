namespace BlogApp.Controllers;

using BlogApp.DTOs;
using BlogApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Common;

[ApiController]
[Route("api/account")]
[Authorize]
public class AccountController(IAccountService service) : ControllerBase
{
    [HttpGet]
    public ActionResult<List<AccountDTO>> GetAllAccounts()
    {
        return Ok(service.GetAllAccounts());
    }

    [HttpGet("{username}")]
    public ActionResult<AccountDTO?> GetAccountByUsername(string username)
    {
        string errCode = "";
        var account = service.GetAccountByUsername(username, ref errCode);

        if (account == null)
        {
            return NotFound(AppMessages.GetMessage(errCode));
        }

        return Ok(account);
    }

    [HttpPost]
    public ActionResult AddAccount([FromBody] AccountDTO accountDTO)
    {
        string errCode = "";
        bool result = service.AddAccount(accountDTO, ref errCode);

        if (!result)
        {
            if (errCode == "E2002")
            {
                return BadRequest(AppMessages.GetMessage(errCode));
            }
            else
            {
                return StatusCode(500,AppMessages.GetMessage(errCode));
            }
        }

        return Ok(AppMessages.GetMessage("I2001"));
    }

    [HttpPut]
    public ActionResult UpdateAccount([FromBody] AccountDTO accountDTO)
    {
        string errCode = "";
        bool result = service.UpdateAccount(accountDTO, ref errCode);

        if (!result)
        {
            if (errCode == "E2001")
            {
                return NotFound(AppMessages.GetMessage(errCode));
            }
            else
            {
                return StatusCode(500,AppMessages.GetMessage(errCode));
            }
        }

        return Ok(AppMessages.GetMessage("I2002"));
    }

    [HttpDelete("{username}")]
    public ActionResult DeleteAccount(string username)
    {
        string errCode = "";
        bool result = service.DeleteAccount(username, ref errCode);

        if (!result)
        {
            if (errCode == "E2001")
            {
                return NotFound(AppMessages.GetMessage(errCode));
            }
            else
            {
                return StatusCode(500,AppMessages.GetMessage(errCode));
            }
        }

        return Ok(AppMessages.GetMessage("I2003"));
    }
}