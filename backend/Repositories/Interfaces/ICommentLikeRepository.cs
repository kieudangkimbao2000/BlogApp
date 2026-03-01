namespace BlogApp.Interfaces;

using BlogApp.Entities;

public interface ICommentLikeRepository
{
    /// <summary>
    ///     Count number of likes for comment
    /// </summary>
    /// <param name="commentId">Comment's ID</param>
    /// <returns>Number of likes for comment</returns>
    int CountNumberOfLikes(string commentId);
    
    /// <summary>
    ///    Add a like
    /// </summary>
    /// <param name="commentLike">Like data</param>
    /// <returns>True if successfully, otherwise false</returns>
    bool AddLike(CommentLike commentLike);

    /// <summary>
    ///   Update a like
    /// </summary>
    /// <param name="commentLike">Like data</param>
    /// <returns>True if successfully, otherwise false</returns>
    bool UpdateLike(CommentLike commentLike);

    /// <summary>
    ///   Delete a like
    /// </summary>
    /// <param name="commentLike">Like data</param>
    /// <returns>True if successfully, otherwise false</returns>
    bool DeleteLike(CommentLike commentLike);
}