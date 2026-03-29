namespace BlogApp.Services;

using BlogApp.Entities;
using BlogApp.Interfaces;
using BlogApp.Mappers;
using BlogApp.DTOs;

/// <summary>
///     Implement Blog Service Interface
/// </summary>
/// <param name="repository"></param>
public class BlogService(IBlogRepository repository): IBlogService
{
    public List<BlogDTO> GetAllBlogs()
    {
        var blogs = repository.GetAllBlogs();
        return blogs.ToDTOList();
    }
    
    public List<BlogDTO> GetBlogsByAuthor(string author)
    {
        var blogs = repository.GetBlogsByAuthor(author);
        return blogs.ToDTOList();
    }

    public BlogDTO GetBlogById(string id, string? userId, ref string errCode)
    {
        var blog = repository.GetBlogById(id);

        if (blog == null) 
        {
            errCode = "E1001"; // Blog not found
            return null;
        }

        var blogDTO = blog.ToDTO();

        if (userId != null)
        {
            bool? like = blog.Likes.FirstOrDefault(bl => bl.AuthorId == userId)?.LikeOrDislike;
            blogDTO.LikedOrDislikedByUser = like;
        }

        return blogDTO;
    }

    public bool AddBlog(BlogDTO blogDTO, ref string errCode)
    {
        bool result = false;
        blogDTO.Id = "blog-"+DateTime.Now.ToString("yyyyMMddHHmmss");

        var blog = repository.GetBlogById(blogDTO.Id);
        if (blog != null) 
        {
            errCode = "E1002"; // Blog with this ID already exists
            return false;
        }

        result = repository.AddBlog(blogDTO.ToModel());

        if (!result)
        {
            errCode = "E1003"; // Failed to add blog
        }

        return result;
    }

    public bool UpdateBlog(BlogDTO blogDTO, ref string errCode)
    {
        bool result = false;
        var blog = repository.GetBlogById(blogDTO.Id);

        if (blog == null) 
        {
            errCode = "E1001"; // Blog not found
            return false;
        }

        result = repository.UpdateBlog(blogDTO.ToModel());

        if (!result)
        {
            errCode = "E1004"; // Failed to update blog
        }

        return result;
    }

    public bool DeleteBlog(string id, ref string errCode)
    {
        bool result = false;
        var blog = repository.GetBlogById(id);

        if (blog == null) 
        {
            errCode = "E1001"; // Blog not found
            return false;
        }

        result = repository.DeleteBlog(blog);

        if (!result)
        {
            errCode = "E1005"; // Failed to delete blog
        }

        return result;
    }

    public RespDTO Get5LatestBlogs()
    {
       List<Blog> result = repository.GetAllBlogs()
                                .Take(5)
                                .ToList(); 

        return new BlogListRespDTO(result.ToDTOList(), 200, " ");
    }

    public RespDTO GetTop5Blogs()
    {
        List<Blog> result = repository.GetAllBlogs()
                                .OrderByDescending(b => b.AmountOfAccesses)
                                .Take(5)
                                .ToList();

        

        return new BlogListRespDTO(result.ToDTOList(), 200, " ");
    }

    public RespDTO SearchBlogs(SearchBlogReqDTO req)
    {
        (List<Blog> blogs, int totalPages) = repository.SearchBlogs(req);


        PageDTO<BlogDTO> blogPage = new PageDTO<BlogDTO>();
        blogPage.CurrPage = (req.CurPage > totalPages) ? totalPages : req.CurPage;
        blogPage.PageSize = totalPages;
        blogPage.Datas = blogs.ToDTOList();
        
        return new SearchBlogRespDTO(blogPage, 200, " ");
    }
}