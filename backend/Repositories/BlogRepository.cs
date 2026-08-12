namespace BlogApp.Repositories;

using BlogApp.Common;
using BlogApp.Dbs;
using BlogApp.DTOs;
using BlogApp.Entities;
using BlogApp.Interfaces;
using Microsoft.EntityFrameworkCore;

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
        var page = req.CurPage > 0 ? req.CurPage : 1;
        const int pageSize = 10;

        var query = context.Blogs.AsQueryable<Blog>().Where(x => x.State == BlogState.PUBLISHED);

        if (!string.IsNullOrWhiteSpace(req.SearchTitle))
        {
            query = query.Where(x => x.Title.Contains(req.SearchTitle));
        }

        if (req.Tags is { Length: > 0 })
        {
            var normalizedTags = req.Tags.Where(tag => !string.IsNullOrWhiteSpace(tag)).ToArray();
            if (normalizedTags.Length > 0)
            {
                query = query.Where(x => x.Tags != null && normalizedTags.Any(tag => x.Tags.Contains(tag)));
            }
        }

        if (req.SearchFlag == 0)
        {
            query = query.OrderByDescending(x => x.PublishedAt);
        }
        else
        {
            query = query.Include(x => x.Likes).OrderByDescending(x => x.Likes.Count());
        }

        var totalItems = query.Count();
        int totalPages = totalItems == 0 ? 0 : (int)Math.Ceiling(totalItems / (double)pageSize);
        List<Blog> blogs = query.Include(x => x.Author)
                                 .Skip((page - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToList();

        return (blogs, totalPages);
    }
}