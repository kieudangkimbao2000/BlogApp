namespace BlogApp.Controllers;

using BlogApp.DTOs;
using BlogApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/// <summary>
///   Tag Controller
/// </summary>
/// <param name="service"></param>
[ApiController]
[Route("api/tags")]
[Authorize]
public class TagController(ITagService service) : ControllerBase
{
    /// <summary>
    ///     Route for retrieving all tags
    /// </summary>
    /// <returns>List of all tags</returns>
    [HttpGet]
    public ActionResult GetAllTags()
    {
        var resp = service.GetAllTags();
            
        return StatusCode(resp.StatusCode, resp);
    }

    [HttpGet("{name}")]
    public ActionResult<TagDTO?> GetTagByName(string name)
    {
        string errCode = "";
        var tag = service.GetTagByName(name, ref errCode);

        if (tag == null)
        {
            return NotFound();
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
                return BadRequest();
            }
            else
            {
                return StatusCode(500,"");
            }
        }

        return Ok();
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
                return NotFound();
            }
            else
            {
                return StatusCode(500,"");
            }
        }

        return Ok();
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
                return NotFound();
            }
            else
            {
                return StatusCode(500,"");
            }
        }

        return Ok();
    }

    /// <summary>
    ///    Route for retrieving the 5 most popular tags
    /// </summary>
    /// <returns>List of 5 most popular tags</returns>
    [AllowAnonymous]
    [HttpGet("topfive")]
    public ActionResult GetTop5Tags()
    {
        var resp = service.GetTop5Tags();

        return StatusCode(resp.StatusCode, resp);
    }
}