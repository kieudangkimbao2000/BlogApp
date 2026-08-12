namespace BlogApp.Controllers;

using BlogApp.DTOs;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/blog")]
public class GenerateInterfaceController : ControllerBase
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
    public ActionResult<RegisterReqDTO> GetRegisterDTO()
    {
        return Ok();
    }

    [HttpGet]
    public ActionResult<AuthenEmailReqDTO> GetVerifyEmailReqDTO()
    {
        return Ok();
    }

    [HttpGet]
    public ActionResult<AuthenEmailRespDTO> GetAuthenEmailRespDTO()
    {
        return Ok();
    }

    [HttpGet]
    public ActionResult<VerifyEmailOTPReqDTO> GetVerifyEmailOTPReqDTO()
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
    [HttpGet]
    public ActionResult<UploadAvatarReqDTO> GetUploadAvatarReqDTO()
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