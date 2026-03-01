namespace BlogApp.Mapper;

using BlogApp.DTOs;
using BlogApp.Entities;

/// <summary>
///   Map between CommentLike Entity and DTO
/// </summary>
public static class CommentLikeMapper
{
    /// <summary>
    ///   Map CommentLike Entity to CommentLike DTO
    /// </summary>
    /// <param name="commentLike">CommentLike Entity Object</param>
    /// <returns>CommentLike DTO Object</returns>
    public static CommentLikeDTO ToDTO(this CommentLike commentLike)
    {
        return new CommentLikeDTO
        {
            AuthorId = commentLike.AuthorId,
            CommentId = commentLike.CommentId,
            LikeOrDislike = commentLike.LikeOrDislike
        };
    }

    /// <summary>
    ///     Map CommentLike DTO to CommentLike Entity
    /// </summary>
    /// <param name="commentLikeDTO">CommentLike DTO Object</param>
    /// <returns>CommentLike Entity Object</returns>
    public static CommentLike ToModel(this CommentLikeDTO commentLikeDTO)
    {
        return new CommentLike
        {
            AuthorId = commentLikeDTO.AuthorId,
            CommentId = commentLikeDTO.CommentId,
            LikeOrDislike = commentLikeDTO.LikeOrDislike
        };
    }

    /// <summary>
    ///    Map List of CommentLike Entity to List of CommentLike DTO
    /// </summary>
    /// <param name="commentLikes">List of CommentLike Entities</param>
    /// <returns>List of CommentLike DTOs/returns>
    public static List<CommentLikeDTO> ToDTOList(this List<CommentLike> commentLikes)
    {
        return commentLikes.Select(cl => cl.ToDTO()).ToList();
    }
}