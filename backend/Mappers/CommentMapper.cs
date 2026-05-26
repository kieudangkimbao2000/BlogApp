namespace BlogApp.Mappers;

using BlogApp.DTOs;
using BlogApp.Entities;

public static class CommentMapper
{
    /// <summary>
    ///     Map Comment Entity to Comment DTO
    /// </summary>
    /// <param name="comment">Comment Entity Object</param>
    /// <returns>Comment DTO Object</returns>
    public static CommentDTO ToDTO(this Comment comment)
    {
        return new CommentDTO
        {
            Id = comment.Id,
            Content = comment.Content,
            State = comment.State,
            AuthorId = comment.AuthorId,
            BlogId = comment.BlogId,
            ParentId = comment.ParentId,
            CreatedAt = comment.CreatedAt
        };
    }

    /// <summary>
    ///    Map Comment DTO to Comment Entity
    /// </summary>
    /// <param name="commentDTO">Comment DTO Object</param>
    /// <returns>Comment Entity Object</returns>
    public static Comment ToModel(this CommentDTO commentDTO)
    {
        return new Comment
        {
            Id = commentDTO.Id,
            Content = commentDTO.Content,
            State = commentDTO.State,
            AuthorId = commentDTO.AuthorId,
            BlogId = commentDTO.BlogId,
            ParentId = commentDTO.ParentId
        };
    }

    /// <summary>
    ///     Map List of Comment Entity to List of Comment DTO
    /// </summary>
    /// <param name="comments">List of Comment Entities</param>
    /// <returns>List of Comment DTOs</returns>
    public static List<CommentDTO> ToDTOList(this List<Comment> comments)
    {
        return comments.Select(c => c.ToDTO()).ToList();
    }
}
