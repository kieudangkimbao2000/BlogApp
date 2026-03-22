namespace BlogApp.Interfaces;

using BlogApp.Mappers;
using BlogApp.DTOs;

/// <summary>
///     Implement business logic for blogs
/// </summary>
public interface IBlogService
{
    /// <summary>
    ///    Get all blogs
    /// </summary>
    /// <returns>List of all blogs</returns>
    List<BlogDTO> GetAllBlogs();

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
    BlogDTO GetBlogById(string id, string? userId, ref string errCode);

    /// <summary>
    ///     Add a blog
    /// </summary>
    /// <param name="blogDTO">Blog data</param>
    /// <param name="errCode">Error code be returned</param>
    /// <returns>True if successfully, otherwise false</returns>
    bool AddBlog(BlogDTO blogDTO, ref string errCode);

    /// <summary>
    ///     Update a blog
    /// </summary>
    /// <param name="blogDTO">Blog data</param>
    /// <param name="errCode">Error code be returned</param>
    /// <returns>True if successfully, otherwise false</returns>
    bool UpdateBlog(BlogDTO blogDTO, ref string errCode);

    /// <summary>
    ///    Delete a blog
    /// </summary>
    /// <param name="id">Blog's Id</param>
    /// <param name="errCode">Error code be returned</param>
    /// <returns>True if successfully, otherwise false</returns>
    bool DeleteBlog(string id, ref string errCode);

    /// <summary>
    ///     Get 5 latest blogs
    /// </summary>
    /// <returns>List of 5 latest blogs</returns>
   RespDTO Get5LatestBlogs();

    /// <summary>
    ///     Get 5 most popular blogs
    /// </summary>
    /// <returns>List of 5 most popular blogs</returns>
    RespDTO GetTop5Blogs();

    /// <summary>
    ///     Search Blogs
    /// </summary>
    /// <param name="req">Search conditions</param>
    /// <returns>List of blogs be suitable to search condtions</returns>
    RespDTO SearchBlogs(SearchBlogReqDTO req);
}


