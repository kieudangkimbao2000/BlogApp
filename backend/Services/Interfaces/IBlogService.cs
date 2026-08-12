namespace BlogApp.Interfaces;

using BlogApp.DTOs;

/// <summary>
///     Implement business logic for blogs
/// </summary>
public interface IBlogService
{
    /// <summary>
    ///     Get blogs by author
    /// </summary>
    /// <param name="author">Author's username</param>
    /// <returns>List of blogs was created by the author</returns>
    List<BlogDTO> GetBlogsByAuthor(string author);

    /// <summary>
    ///   Get blogs by id
    /// </summary>
    /// <param name="id">Blog's Id</param>
    /// <param name="errCode">Error code be returned</param>
    /// <returns>Blog if found, otherwise null</returns>
    ResponseBaseDTO GetBlogById(string id);

    /// <summary>
    ///     Add a blog
    /// </summary>
    /// <param name="blogDTO">Blog data</param>
    /// <returns>True if successfully, otherwise false</returns>
    Task<ResponseBaseDTO> AddBlog(BlogDTO req, string[] base64Strings);

    /// <summary>
    ///     Update a blog
    /// </summary>
    /// <param name="blogDTO">Blog data</param>
    /// <param name="errCode">Error code be returned</param>
    /// <returns>True if successfully, otherwise false</returns>
    Task<ResponseBaseDTO> UpdateBlog(BlogDTO blogDTO);

    /// <summary>
    ///    Delete a blog
    /// </summary>
    /// <param name="id">Blog's Id</param>
    /// <param name="errCode">Error code be returned</param>
    /// <returns>True if successfully, otherwise false</returns>
    Task<ResponseBaseDTO> DeleteBlog(string id);

    /// <summary>
    ///     Get 5 latest blogs
    /// </summary>
    /// <returns>List of 5 latest blogs</returns>
    ResponseBaseDTO Get5LatestBlogs();

    /// <summary>
    ///     Get 5 most popular blogs
    /// </summary>
    /// <returns>List of 5 most popular blogs</returns>
    ResponseBaseDTO GetTop5Blogs();

    /// <summary>
    ///     Search Blogs
    /// </summary>
    /// <param name="req">Search conditions</param>
    /// <returns>List of blogs be suitable to search condtions</returns>
    ResponseBaseDTO SearchBlogs(SearchBlogReqDTO req);

    /// <summary>
    ///    Get blog details
    /// </summary>
    /// <param name="id">Blog's Id</param>
    /// <param name="username">User's username (optional)</param>
    /// <returns>Blog details if found, otherwise null</returns>
    ResponseBaseDTO GetBlogDetails(string id, string? username);
}


