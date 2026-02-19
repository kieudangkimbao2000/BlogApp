namespace BlogApp.Controllers;

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
        var blog = service.GetBlogById(id);

        if (blog == null) return NotFound("Blog not found!");

        return Ok(blog);
    }

    [HttpPost]
    public ActionResult AddBlog([FromBody] BlogDTO blogDTO)
    {
        var success = service.AddBlog(blogDTO);

        if (!success) return BadRequest("Blog with the same ID already exists!");

        return Ok("Blog added successfully!");
    }

    [HttpPost("update")]
    public ActionResult UpdateBlog([FromBody] BlogDTO blogDTO)
    {
        var success = service.UpdateBlog(blogDTO);

        if (!success) return NotFound("Blog not found!");

        return Ok("Blog updated successfully!");
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteBlog(string id)
    {
        var success = service.DeleteBlog(id);

        if (!success) return NotFound("Blog not found!");

        return Ok("Blog deleted successfully!");
    }
}