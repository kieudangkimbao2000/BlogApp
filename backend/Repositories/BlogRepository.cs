namespace BlogApp.Repositories;

using BlogApp.Dbs;
using BlogApp.DTOs;
using BlogApp.Entities;
using BlogApp.Interfaces;

/// <summary>
///    Implement Blog Repository Interface
/// </summary>
/// <param name="context"></param>
/// <param name="logger"></param>
public class BlogRepository(BlogAppContext context, 
                            ILogger<BlogRepository> logger) : IBlogRepository
{
    public List<Blog> GetAllBlogs()
    {
        return context.Blogs.OrderByDescending(b => b.CreatedAt).ToList();
    }

    public List<Blog> GetBlogsByAuthor(string author)
    {
        return context.Blogs.Where(b => b.AuthorId == author)
                            .OrderByDescending(b => b.CreatedAt).ToList();
    }

    public Blog? GetBlogById(string id)
    {
        return context.Blogs.FirstOrDefault(b => b.Id  == id);
    }

    public bool AddBlog(Blog blog)
    {
        try
        {
            context.Blogs.Add(blog);
            context.SaveChanges();

            return true;
        } catch(Exception ex)
        {
            logger.LogError(ex, "Error adding blog with id {BlogId}", blog.Id);
            return false;
        }
    }

    public bool UpdateBlog(Blog blog)
    {
        try
        {
            context.Blogs.Update(blog);
            context.SaveChanges();

            return true;
        } catch(Exception ex)
        {
            logger.LogError(ex, "Error updating blog with id {BlogId}", blog.Id);
            return false;
        }
    }

    public bool DeleteBlog(Blog blog)
    {
        try
        {
            context.Blogs.Remove(blog);
            context.SaveChanges();

            return true;
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Error deleting blog with id {BlogId}", blog.Id);
            return false;
        }
    }

    public (List<Blog>, int) SearchBlogs(SearchBlogReqDTO req)
    {
        var query = context.Blogs.Where(x => (req.SearchTitle != "" ? x.Title.Contains(req.SearchTitle) : true) && 
                                            ((req.Categories != null && req.Categories.Length > 0) ? x.Categories.ContainsAny(req.Categories) : true)
                                        );
        if(req.SearchFlag == 0)
        {
            query = query.OrderByDescending(x => x.PublishedAt);
        }
        else
        {
            query = query.OrderByDescending(x => x.Likes.Count());
        }


        int totalPages = (int)Math.Ceiling((decimal)(query.Count()/10));
        List<Blog> blogs = query.Skip(10*(req.CurPage - 1)).Take(10).ToList();

        return (blogs, totalPages);
    }
}