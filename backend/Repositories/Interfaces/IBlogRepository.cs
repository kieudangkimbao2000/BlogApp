namespace BlogApp.Interfaces;

using BlogApp.Entities;

public interface IBlogRepository
{
    List<Blog> GetAllBlogs();

    Blog? GetBlogById(string id);

    bool AddBlog(Blog blog);

    bool UpdateBlog(Blog blog);

    bool DeleteBlog(Blog blog);
}