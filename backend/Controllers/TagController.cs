namespace BlogApp.Controllers;

using BlogApp.Common;
using BlogApp.DTOs;
using BlogApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/tags")]
[Authorize]
public class TagController(ITagService service) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public ActionResult<TagListRespDTO> GetAllTags()
    {
        return Ok(service.GetAllTags());
    }

    [HttpGet("{name}")]
    public ActionResult<TagDTO?> GetTagByName(string name)
    {
        string errCode = "";
        var tag = service.GetTagByName(name, ref errCode);

        if (tag == null)
        {
            return NotFound(AppMessages.GetMessage(errCode));
        }

        return Ok(tag);
    }

    [HttpPost]
    public ActionResult AddTag([FromBody] TagDTO tagDTO)
    {
        string errCode = "";
        bool result = service.AddTag(tagDTO, ref errCode);

        if (!result)
        {
            if (errCode == "E3002")
            {
                return BadRequest(AppMessages.GetMessage(errCode));
            }
            else
            {
                return StatusCode(500,AppMessages.GetMessage(errCode));
            }
        }

        return Ok(AppMessages.GetMessage("I3001"));
    }

    [HttpPut]
    public ActionResult UpdateTag([FromBody] TagDTO tagDTO)
    {
        string errCode = "";
        bool result = service.UpdateTag(tagDTO, ref errCode);

        if (!result)
        {
            if (errCode == "E3001")
            {
                return NotFound(AppMessages.GetMessage(errCode));
            }
            else
            {
                return StatusCode(500,AppMessages.GetMessage(errCode));
            }
        }

        return Ok(AppMessages.GetMessage("I3002"));
    }

    [HttpDelete("{name}")]
    public ActionResult DeleteTag(string name)
    {
        string errCode = "";
        bool result = service.DeleteTag(name, ref errCode);

        if (!result)
        {
            if (errCode == "E3001")
            {
                return NotFound(AppMessages.GetMessage(errCode));
            }
            else
            {
                return StatusCode(500,AppMessages.GetMessage(errCode));
            }
        }

        return Ok(AppMessages.GetMessage("I3003"));
    }

    [AllowAnonymous]
    [HttpGet("topfive")]
    public ActionResult GetTop5Tags()
    {
        var resp = service.GetTop5Tags();

        return Ok(resp);
    }
}