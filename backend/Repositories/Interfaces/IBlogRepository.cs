namespace BlogApp.Interfaces;

using BlogApp.DTOs;
using BlogApp.Entities;

/// <summary>
///     Interact with Blogs in the database
/// </summary>
public interface IBlogRepository
{
    /// <summary>
    ///    Get all blogs
    /// </summary>
    /// <returns>List of all blogs ordered by creation date descending</returns>
    List<Blog> GetAllBlogs();

    /// <summary>
    ///    Get all blogs by an author
    /// </summary>
    /// <param name="author">The author of blog</param>
    /// <returns>List of blogs created by the author</returns>
    List<Blog> GetBlogsByAuthor(string author);

    /// <summary>
    ///   Get a blog by id
    /// </summary>
    /// <param name="id">Blog's id</param>
    /// <returns>The blog if found, otherwise null</returns>
    Blog? GetBlogById(string id);

    /// <summary>
    ///     Add a new blog to the database
    /// </summary>
    /// <param name="blog">The blog to be added</param>
    /// <returns>True if successfully, otherwise false</returns>
    bool AddBlog(Blog blog);

    /// <summary>
    ///   Update an existing blog in the database
    /// </summary>
    /// <param name="blog">The blog to be updated</param>
    /// <returns>True if successfully, otherwise false</returns>
    bool UpdateBlog(Blog blog);

    /// <summary>
    ///     Delete a blog from the database
    /// </summary>
    /// <param name="blog">The blog to be deleted</param>
    /// <returns>True if successfully, otherwise false</returns>
    bool DeleteBlog(Blog blog);

    /// <summary>
    ///     Search Blogs
    /// </summary>
    /// <param name="req">search blogs conditions</param>
    /// <returns>List of blogs be suitable to search conditions </returns>
    (List<Blog>, int) SearchBlogs(SearchBlogReqDTO req);

    /// <summary>
    ///    Get the blog that is being edited by an author
    /// </summary>
    /// <param name="username">The author</param>
    /// <returns>Being edited blog if found, otherwise null</returns>
    Blog? GetBeingEditedBlog(string username);
}