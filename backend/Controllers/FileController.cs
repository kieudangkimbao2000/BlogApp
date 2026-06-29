using BlogApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers;

[ApiController]
[Route("api/file")]
public class FileController(IFileService service) : ControllerBase
{
    [HttpGet("img/blog/{username}/{fileName}")]
    public async Task<ActionResult> GetCoverImg(string username, string fileName)
    {
        byte[] fileBin = await service.GetCoverImg(username, fileName);

        return File(fileBin, "application/png");
    }
}