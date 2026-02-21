namespace BlogApp.Controllers;

using BlogApp.Common;
using BlogApp.DTOs;
using BlogApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/blog")]
[Authorize]
public class BlogController(IBlogService service) : ControllerBase
{
    [HttpGet]
    public ActionResult<List<BlogDTO>> GetAllBlogs()
    {
        var blogs = service.GetAllBlogs();
        return Ok(blogs);
    }
    [HttpGet("{id}")]
    public ActionResult<BlogDTO> GetBlogById(string id)
    {
        string errCode = "";
        var blog = service.GetBlogById(id, ref errCode);

        if (blog == null) return NotFound(AppMessages.GetMessage(errCode));

        return Ok(blog);
    }

    [HttpPost]
    public ActionResult AddBlog([FromBody] BlogDTO blogDTO)
    {
        string errCode = "";
        var result = service.AddBlog(blogDTO, ref errCode);

        if (!result)
        {
            if (errCode == "E1002")
            {
                return BadRequest(AppMessages.GetMessage(errCode));
            }
            else
            {
                return StatusCode(500,AppMessages.GetMessage(errCode));
            }
        }

        return Ok(AppMessages.GetMessage("I1001"));
    }

    [HttpPost("update")]
    public ActionResult UpdateBlog([FromBody] BlogDTO blogDTO)
    {
        string errCode = "";
        var result = service.UpdateBlog(blogDTO, ref errCode);

        if (!result){
            if (errCode == "E1001")
            {
                return NotFound(AppMessages.GetMessage(errCode));
            }
            else
            {
                return StatusCode(500,AppMessages.GetMessage(errCode));
            }
        
        }
        return Ok(AppMessages.GetMessage("I1002"));
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteBlog(string id)
    {
        string errCode = "";
        var result = service.DeleteBlog(id, ref errCode);

        if (!result){
            if (errCode == "E1001")
            {
                return NotFound(AppMessages.GetMessage(errCode));
            }
            else
            {
                return StatusCode(500,AppMessages.GetMessage(errCode));
            }
        }

        return Ok(AppMessages.GetMessage("I1003"));
    }
}