namespace BlogApp.Interfaces;

using BlogApp.Entities;

/// <summary>
///     Interact with BlogLikes in the database
/// </summary>
public interface IBlogLikeRepository
{
    /// <summary>
    ///     Count number of likes for blog
    /// </summary>
    /// <param name="blogId">Blog's ID</param>
    /// <returns>Number of likes for blog</returns>
    int CountNumberOfLikes(string blogId);
    
    /// <summary>
    ///    Add a like
    /// </summary>
    /// <param name="blogLike">Like data</param>
    /// <returns>True if successfully, otherwise false</returns>
    bool AddLike(BlogLike blogLike);

    /// <summary>
    ///   Update a like
    /// </summary>
    /// <param name="blogLike">Like data</param>
    /// <returns>True if successfully, otherwise false</returns>
    bool UpdateLike(BlogLike blogLike);

    /// <summary>
    ///   Delete a like
    /// </summary>
    /// <param name="blogLike">Like data</param>
    /// <returns>True if successfully, otherwise false</returns>
    bool DeleteLike(BlogLike blogLike);
}