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
        var blog = service.GetBlogById(id, null, ref errCode);

        if (blog == null) return NotFound(AppMessages.GetMessage(errCode));

        return Ok(blog);
    }

    [HttpPost("add")]
    public async Task<ActionResult> AddBlog([FromForm] BlogReqDTO req)
    {
        var result = await service.AddBlog(req);

        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("update")]
    public async Task<ActionResult> UpdateBlog([FromForm] BlogReqDTO req)
    {
        var result = await service.UpdateBlog(req);
        
        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBlog(string id)
    {
        var result = await service.DeleteBlog(id);

        return StatusCode(result.StatusCode, result);
    }

    [AllowAnonymous]
    [HttpGet("fivelatest")]
    public ActionResult Get5LatestBlogs()
    {
        var resp = service.Get5LatestBlogs();

        return Ok(resp);
    }

    [AllowAnonymous]
    [HttpGet("topfive")]
    public ActionResult GetTop5Blogs()
    {
        var resp = service.GetTop5Blogs();

        return Ok(resp);
    }

    [AllowAnonymous]
    [HttpPost("search")]
    public ActionResult SearchBlogs([FromForm]SearchBlogReqDTO req)
    {
        var resp = service.SearchBlogs(req);

         return Ok(resp);
    }

    [HttpGet("being-edited-blog/{username}")]
    public ActionResult GetBeingEditedBlog(string username)
    {
        if (username == null)
        {
            return Unauthorized(AppMessages.GetMessage("E1001"));
        }

        var resp = service.GetBeingEditedBlog(username);

        if (resp.StatusCode == 404)
        {
            return NotFound(resp);
        }

        return Ok(resp);
    }
}