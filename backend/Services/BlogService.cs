namespace BlogApp.Services;

using BlogApp.Entities;
using BlogApp.Interfaces;
using BlogApp.Mappers;
using BlogApp.DTOs;
using BlogApp.DTOs;
using BlogApp.Handlers;

/// <summary>
///     Implement Blog Service Interface
/// </summary>
/// <param name="repository"></param>
public class BlogService(IBlogRepository repository,
                            FileHandler handler): IBlogService
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
            bool? like = blog.Likes.FirstOrDefault(bl => bl.AuthorId == userId)?.LikeOrNot;
            blogDTO.LikedByUser = like;
        }

        return blogDTO;
    }

    public async Task<ResponseBaseDTO> AddBlog(BlogReqDTO req)
    {
        bool result = false;
        BlogDTO blog = req.Blog;
        blog.Id = "blog-"+DateTime.Now.ToString("yyyyMMddHHmmss");

        // if(req.files != null && req.files.Length > 0)
        // {
        //     string fileName = blog.Id + ".png";
        //     bool resultSave = await handler.SaveImgFile(blog.AuthorId, fileName, "blogs", req.files[0]);
        //     if(!resultSave)
        //     {
        //         return new ResponseBaseDTO(stat: 500, "A error occured when add a blog!");
        //     }
        //     blog.CoverImage = Path.Combine(blog.AuthorId, fileName);
        // }

        var exBlog = repository.GetBlogById(blog.Id);
        if (blog != null) 
        {
            return new ResponseBaseDTO(stat: 409, "Blog's already exist!");
        }

        result = repository.AddBlog(blog.ToModel());
        
        if(!result)
        {
            return new ResponseBaseDTO(stat: 500, "A error occured when add a blog!");
        }

        return new ResponseBaseDTO(stat: 200, "");
    }

    public async Task<ResponseBaseDTO> UpdateBlog(BlogReqDTO req)
    {
        BlogDTO blog = req.Blog;
        var exsBlog = repository.GetBlogById(blog.Id);

        if (exsBlog == null) 
        {
            return new ResponseBaseDTO(403, "Blog's not found!");
        }

        // if(req.files != null && req.files.Length > 0)
        // {
        //     string fileName = blog.Id + ".png";
        //     var resultSave = await handler.SaveImgFile(blog.AuthorId, fileName, "blogs", req.files[0]);
        //     if(!resultSave)
        //     {
        //         return new ResponseBaseDTO(500, "A error occured when updating the blog!");
        //     }
        //     blog.CoverImage = Path.Combine(blog.AuthorId, fileName);
        // }

        var result = repository.UpdateBlog(blog.ToModel());

        if (!result)
        {
            return new ResponseBaseDTO(403, "Blog's not found!");
        }

        return new ResponseBaseDTO(200, "");;
    }

    public async Task<ResponseBaseDTO> DeleteBlog(string id)
    {
        var blog = repository.GetBlogById(id);

        if (blog == null) 
        {
            return new ResponseBaseDTO(403, "Blog's not found!");
        }

        string fileName = Path.GetFileName(blog.CoverImage);
        var resultDel = handler.DeleteImgFile(blog.AuthorId, fileName, "blogs");
        if(!resultDel)
        {
            return new ResponseBaseDTO(500, "A error occured when deleting the blog!");
        }

        var result = repository.DeleteBlog(blog);

        if (!result)
        {
            return new ResponseBaseDTO(500, "A error occured when deleting the blog!");
        }

        return new ResponseBaseDTO(200, "");
    }

    public ResponseBaseDTO Get5LatestBlogs()
    {
       List<Blog> result = repository.GetAllBlogs()
                                .Take(5)
                                .ToList(); 

        return new BlogListRespDTO(result.ToDTOList(), 200, " ");
    }

    public ResponseBaseDTO GetTop5Blogs()
    {
        List<Blog> result = repository.GetAllBlogs()
                                .OrderByDescending(b => b.AmountOfAccesses)
                                .Take(5)
                                .ToList();

        

        return new BlogListRespDTO(result.ToDTOList(), 200, " ");
    }

    public ResponseBaseDTO GetBeingEditedBlog(string username)
    {
        var blog = repository.GetBeingEditedBlog(username);

        if (blog == null)
        {
            return new ResponseBaseDTO(404, "Blog not found");
        }

        return new BlogRespDTO(blog.ToDTO(), 200, " ");
    }

    public ResponseBaseDTO SearchBlogs(SearchBlogReqDTO req)
    {
        (List<Blog> blogs, int totalPages) = repository.SearchBlogs(req);


        PageDTO<BlogDTO> blogPage = new PageDTO<BlogDTO>();
        blogPage.CurPage = (req.CurPage > totalPages) ? totalPages : req.CurPage;
        blogPage.PageSize = totalPages;
        blogPage.Datas = blogs.ToDTOList();
        
        return new BlogPageRespDTO(blogPage, 200, " ");
    }
}