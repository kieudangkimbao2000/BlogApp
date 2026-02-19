namespace BlogApp.Services;

using BlogApp.Entities;
using BlogApp.Interfaces;
using BlogApp.Mappers;
using BlogApp.DTOs;
using BlogApp.Repositories;

public class BlogService(IBlogRepository repository): IBlogService
{
    public List<BlogDTO> GetAllBlogs()
    {
        var blogs = repository.GetAllBlogs();
        return blogs.ToDTOList();
    }

    public BlogDTO GetBlogById(string id)
    {
        var blog = repository.GetBlogById(id);
        if (blog == null) return null;

        return blog.ToDTO();
    }

    public bool AddBlog(BlogDTO blogDTO)
    {
        blogDTO.Id = "blog-"+DateTime.Now.ToString("yyyyMMddHHmmss");

        var blog = repository.GetBlogById(blogDTO.Id);
        if (blog != null) return false;

        return repository.AddBlog(blogDTO.ToModel());
    }

    public bool UpdateBlog(BlogDTO blogDTO)
    {
        var blog = repository.GetBlogById(blogDTO.Id);
        if (blog == null) return false;

        return repository.UpdateBlog(blogDTO.ToModel());
    }

    public bool DeleteBlog(string id)
    {
        var blog = repository.GetBlogById(id);
        if (blog == null) return false;

        return repository.DeleteBlog(blog);
    }
}