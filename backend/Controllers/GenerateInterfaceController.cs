namespace BlogApp.Controllers;

using BlogApp.Common;
using BlogApp.DTOs;
using BlogApp.Entities;
using BlogApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
    public ActionResult<PageDTO<T>> GetPageDTO<T>()
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
    public ActionResult<LoginDTO> GetLoginDTO()
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
    public ActionResult<ReqDTO> GetReqDTO()
    {
        return Ok();
    }

    [HttpGet]
    public ActionResult<SearchBlogReqDTO> GetSearchBlogReqDTO()
    {
        return Ok();
    }
    #endregion

    #region Response DTOs
    [HttpGet]
    public ActionResult<BlogListRespDTO> GetBlogListRespDTO()
    {
        return Ok();
    }

    [HttpGet]
    public ActionResult<BlogPageRespDTO> GetBlogPageRespDTO()
    {
        return Ok();
    }

    [HttpGet]
    public ActionResult<LoginRespDTO> GetLoginRespDTO()
    {
        return Ok();
    }

    [HttpGet]
    public ActionResult<RegisterRespDTO> GetRegisterRespDTO()
    {
        return Ok();
    }
    
    [HttpGet]
    public ActionResult<RespDTO> GetRespDTO()
    {
        return Ok();
    }

    [HttpGet]
    public ActionResult<SearchBlogRespDTO> GetSearchBlogRespDTO()
    {
        return Ok();
    }

    [HttpGet]
    public ActionResult<TagListRespDTO> GetTagListRespDTO()
    {
        return Ok();
    }
    #endregion

}