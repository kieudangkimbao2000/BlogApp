using BlogApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers;

/// <summary>
///     File Controller
/// </summary>
/// <param name="service"></param>
[ApiController]
[Route("api/file")]
public class FileController(IFileService service) : ControllerBase
{
    /// <summary>
    ///   Route for getting a blog cover image
    /// </summary>
    /// <param name="username"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    [HttpGet("img/blog/{username}/{fileName}")]
    public async Task<ActionResult> GetCoverImg(string username, string fileName)
    {
        byte[] fileBin = await service.GetCoverImg(username, fileName);

        return File(fileBin, "image/png");
    }

    [HttpGet("img/profile/{username}/{fileName}")]
    public async Task<ActionResult> GetProfileImg(string username, string fileName)
    {
        byte[] fileBin = await service.GetProfileImg(username, fileName);

        return File(fileBin, "image/png");
    }
}