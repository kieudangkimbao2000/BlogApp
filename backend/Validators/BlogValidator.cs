namespace BlogApp.Validators;

using BlogApp.DTOs;
using BlogApp.Entities;
using BlogApp.Interfaces;

public class BlogValidator(IHttpContextAccessor httpContextAccessor, 
                            IBlogRepository repository) : IBlogValidator
{
    /// <summary>
    ///   Store the username of the user making the request
    /// </summary>
    private readonly string username = httpContextAccessor.HttpContext?.User?.Identity?.Name ?? string.Empty;

    public bool ValidateSearchBlogRequest(SearchBlogReqDTO search)
    {
        // validate the search object is not null
        if (search == null)
        {
            return false;
        }

        return true;
    }
    
    public bool ValidateAddBlogRequest(BlogDTO blog)
    {
        // validate the blog object is not null
        if (blog == null)
        {
            return false;
        }

        // Validate required fields
        if(string.IsNullOrWhiteSpace(blog.Title) || string.IsNullOrWhiteSpace(blog.Content) || blog.Tags == null || blog.Tags.Length == 0 || 
            string.IsNullOrWhiteSpace(blog.AuthorId))
        {
            return false;
        }

        // Validate the author ID
        if(blog.AuthorId != username)
        {
            return false;
        }

        return true;
    }

    public  bool ValidateDeleteBlogRequest(BlogDTO blog)
    {
        // validate the blog object is not null
        if(blog == null || string.IsNullOrWhiteSpace(blog.Id))
        {
            return false;
        }

        return true;
    }

    public bool ValidateBeforeDeleteBlog(Blog blog)
    {
        //Check exist
        if(blog == null)
        {
            return false;
        }

        // Validate the author ID
        if(blog.AuthorId != username)
        {
            return false;
        }

        return true;
    }

    public bool ValidateUpdateBlogRequest(BlogDTO blog)
    {
        // validate the blog object is not null
        if(blog == null || string.IsNullOrWhiteSpace(blog.Id))
        {
            return false;
        }

        //Check exist
        var existingBlog = repository.GetBlogById(blog.Id);
        if(existingBlog == null)
        {
            return false;
        }

        // Validate the author ID
        if(existingBlog.AuthorId != username)
        {
            return false;
        }

        return true;
    }
}