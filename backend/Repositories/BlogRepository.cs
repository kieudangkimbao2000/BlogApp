namespace BlogApp.Repositories;

using BlogApp.Dbs;
using BlogApp.Entities;
using BlogApp.Interfaces;

public class BlogRepository(BlogAppContext context) : IBlogRepository
{
    public List<Blog> GetAllBlogs()
    {
        return context.Blogs.OrderBy(b => b.CreatedAt).ToList();
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
            return false;
        }
    }
}