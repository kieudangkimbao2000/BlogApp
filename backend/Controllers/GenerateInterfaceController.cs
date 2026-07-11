namespace BlogApp.Controllers;

using BlogApp.DTOs;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/blog")]
public class GenerateInterfaceController() : ControllerBase
{
    #region Main DTOs
    [HttpGet]
    public ActionResult<AccountDTO> GetAccountDTO()
    {
        return Ok();
    }

    [HttpGet]
    public ActionResult<BlogDTO> GetBlogDTO()
    {
        return Ok();
    }

    [HttpGet]
    public ActionResult<BlogLikeDTO> GetBlogLikeDTO()
    {
        return Ok();
    }

    [HttpGet]
    public ActionResult<CommentDTO> GetCommentDTO()
    {
        return Ok();
    }

    [HttpGet]
    public ActionResult<CommentLikeDTO> GetCommentLikeDTO()
    {
        return Ok();
    }

    [HttpGet]
    public ActionResult<NotificationDTO> GetNotificationDTO()
    {
        return Ok();
    }

    [HttpGet]
    public ActionResult<ReportDTO> GetReportDTO()
    {
        return Ok();
    }

    [HttpGet]
    public ActionResult<TagDTO> GetTagDTO()
    {
        return Ok();
    }
    #endregion

    #region Authentication DTOs
    [HttpGet]
    public ActionResult<LoginReqDTO> GetLoginDTO()
    {
        return Ok();
    }

    [HttpGet]
    public ActionResult<RegisterDTO> GetRegisterDTO()
    {
        return Ok();
    }
    #endregion

    #region Request DTOs
    [HttpGet]
    public ActionResult<SearchBlogReqDTO> GetSearchBlogReqDTO()
    {
        return Ok();
    }
    #endregion

    #region Response DTOs
        [HttpGet]
    public ActionResult<LoginRespDTO> GetLoginRespDTO()
    {
        return Ok();
    }
    public ActionResult<ErrorRespDTO> GetResponseBaseDTO()
    {
        return Ok();
    }
    #endregion
}