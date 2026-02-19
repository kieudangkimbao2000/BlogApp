namespace BlogApp.Interfaces;

using BlogApp.Mappers;
using BlogApp.DTOs;

public interface IBlogService
{
    List<BlogDTO> GetAllBlogs();

    BlogDTO GetBlogById(string id);

    bool AddBlog(BlogDTO blogDTO);

    bool UpdateBlog(BlogDTO blogDTO);

    bool DeleteBlog(string id);
}


