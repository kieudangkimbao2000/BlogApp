namespace BlogApp.Interfaces;
using BlogApp.DTOs;
using BlogApp.Entities;


/// <summary>
///   Blog Validator
/// </summary>
public interface IBlogValidator
{
    /// <summary>
    ///   Validates the search blog request
    /// </summary>
    /// <param name="search">The search blog request to validate</param>
    /// <returns>True if the request is valid, otherwise false</returns>
    bool ValidateSearchBlogRequest(SearchBlogReqDTO search);

    /// <summary>
    ///   Validates the add blog request
    /// </summary>
    /// <param name="blog">The add blog request to validate</param>
    /// <returns>True if the request is valid, otherwise false</returns>
    bool ValidateAddBlogRequest(BlogDTO blog);

    /// <summary>
    ///   Validates the delete blog request
    /// </summary>
    /// <param name="blog">The delete blog request to validate</param>
    /// <returns>True if the request is valid, otherwise false</returns>
    bool ValidateDeleteBlogRequest(BlogDTO blog);

    /// <summary>
    ///  Validates the delete blog request before deleting the blog
    /// </summary>
    /// <param name="blog">The blog to be deleted</param>
    /// <param name="userId">The ID of the user attempting to delete the blog</param>
    /// <returns>True if the blog can be deleted by the user, otherwise false</returns>
    bool ValidateBeforeDeleteBlog(Blog blog);
}