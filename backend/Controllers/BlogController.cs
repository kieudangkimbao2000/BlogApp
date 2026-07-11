namespace BlogApp.Controllers;

using BlogApp.DTOs;
using BlogApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/// <summary>
///  Blog Controller
/// </summary>
/// <param name="service"></param>
[ApiController]
[Route("api/blog")]
[Authorize]
public class BlogController(IBlogService service) : ControllerBase
{
    [HttpGet("{id}")]
    public ActionResult<BlogDTO> GetBlogById(string id)
    {
        string errCode = "";
        var blog = service.GetBlogById(id, null, ref errCode);

        if (blog == null) return NotFound();

        return Ok(blog);
    }

    /// <summary>
    ///   Route for adding a new blog
    /// </summary>
    /// <param name="req">Add blog request</param>
    /// <returns>Status Code</returns>
    [HttpPost("add")]
    public async Task<ActionResult> AddBlog([FromBody] RequestBaseDTO<BlogDTO> req)
    {
        var result = await service.AddBlog(req.Datas, req.Base64Strings ?? []);

        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    ///     Route for updating an blog
    /// </summary>
    /// <param name="req">Update blog request</param>
    /// <returns>Status Code</returns>
    [HttpPost("update")]
    public async Task<ActionResult> UpdateBlog([FromBody] RequestBaseDTO<BlogDTO> req)
    {
        var result = await service.UpdateBlog(req.Datas);

        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    ///   Route for deleting a blog
    /// </summary>
    /// <param name="id">Blog ID</param>
    /// <returns>Status Code</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBlog(string id)
    {
        var result = await service.DeleteBlog(id);

        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    ///     Route for retrieving the 5 latest blogs
    /// </summary>
    /// <returns>List of 5 latest blogs</returns>
    [AllowAnonymous]
    [HttpGet("fivelatest")]
    public ActionResult Get5LatestBlogs()
    {
        var resp = service.Get5LatestBlogs();

        return Ok(resp);
    }

    /// <summary>
    ///     Route for retrieving the 5 most popular blogs
    /// </summary>
    /// <returns>List of 5 most popular blogs</returns>
    [AllowAnonymous]
    [HttpGet("topfive")]
    public ActionResult GetTop5Blogs()
    {
        var resp = service.GetTop5Blogs();

        return Ok(resp);
    }

    /// <summary>
    ///    Route for searching blogs
    /// </summary>
    /// <param name="req">Search blog request</param>
    /// <returns>List of matching blogs</returns>
    [AllowAnonymous]
    [HttpPost("search")]
    public ActionResult SearchBlogs([FromBody] RequestBaseDTO<SearchBlogReqDTO> req)
    {
        var resp = service.SearchBlogs(req.Datas);

        return Ok(resp);
    }
}