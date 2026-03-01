namespace BlogApp.Interfaces;

using BlogApp.Entities;


public interface ICommentRepository
{
    /// <summary>
    ///     Get all comments of a blog
    /// </summary>
    /// <param name="blogId">The blog's id</param>
    /// <returns>List of comments ordered by creation date descending</returns>
    List<Comment> GetCommentsByBlogId(string blogId);

    /// <summary>
    ///     Add a comment to the database
    /// </summary>
    /// <param name="comment">Comment data</param>
    /// <returns>True if successfully, otherwise false</returns>
    bool AddComment(Comment comment);

    /// <summary>
    ///    Update a comment in the database
    /// </summary>
    /// <param name="comment">Comment data</param>
    /// <returns>True if successfully, otherwise false</returns>
    bool UpdateComment(Comment comment);

    /// <summary>
    ///     Delete a comment from the database
    /// </summary>
    /// <param name="comment">Comment data</param>
    /// <returns>True if successfully, otherwise false</returns>
    bool DeleteComment(Comment comment);
}