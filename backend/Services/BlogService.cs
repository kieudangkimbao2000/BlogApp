namespace BlogApp.Services;

using BlogApp.Entities;
using BlogApp.Interfaces;
using BlogApp.Mappers;
using BlogApp.DTOs;
using BlogApp.Handlers;
using Azure;
using BlogApp.Common;

/// <summary>
///     Implement Blog Service Interface
/// </summary>
/// <param name="repository"></param>
public class BlogService(IBlogRepository repository,
                            FileHandler handler,
                            IBlogValidator validator): IBlogService
{
    public List<BlogDTO> GetBlogsByAuthor(string author)
    {
        var blogs = repository.GetBlogsByAuthor(author);
        return blogs.ToDTOList();
    }

    public ResponseBaseDTO GetBlogById(string id)
    {
        var blog = repository.GetBlogById(id);

        if (blog == null) 
        {
            return new ResponseBaseDTO<ErrorRespDTO>(new ErrorRespDTO(AppMessages.E1001), 404);
        }

        var blogDTO = blog.ToDTO();

        return new ResponseBaseDTO<BlogDTO>(blogDTO, 200);
    }

    public async Task<ResponseBaseDTO> AddBlog(BlogDTO blog, string[] base64Strings)
    {
        bool result = false;
        blog.Id = "blog-"+DateTime.Now.ToString("yyyyMMddHHmmss");

        if(base64Strings != null && base64Strings.Length > 0)
        {
            string fileName = blog.Id + ".png";
            bool resultSave = await handler.SaveImgFile(blog.AuthorId, fileName, "blogs", base64Strings[0]);
            if(!resultSave)
            {
                return new ResponseBaseDTO<ErrorRespDTO>(new ErrorRespDTO(AppMessages.E1003), 500);
            }
            blog.CoverImage = Path.Combine(blog.AuthorId, fileName);
        }

        if(!validator.ValidateAddBlogRequest(blog))
        {
            return new ResponseBaseDTO(400);
        }
        
        result = repository.AddBlog(blog.ToModel());
        
        if(!result)
        {
            return new ResponseBaseDTO<ErrorRespDTO>(new ErrorRespDTO(AppMessages.E1003), 500);
        }

        return new ResponseBaseDTO<BlogDTO>(blog, stat: 200);
    }

    public async Task<ResponseBaseDTO> UpdateBlog(BlogDTO blog)
    {
        var exsBlog = repository.GetBlogById(blog.Id);

        if (exsBlog == null)
        {
            return new ResponseBaseDTO<ErrorRespDTO>(new ErrorRespDTO(AppMessages.E1001), 403);
        }

        // if(req.files != null && req.files.Length > 0)
        // {
        //     string fileName = blog.Id + ".png";
        //     var resultSave = await handler.SaveImgFile(blog.AuthorId, fileName, "blogs", req.files[0]);
        //     if(!resultSave)
        //     {
        //         return new ResponseBaseDTO<ErrorRespDTO>(new ErrorRespDTO(AppMessages.E1005), 500);
        //     }
        //     blog.CoverImage = Path.Combine(blog.AuthorId, fileName);
        // }

        var result = repository.UpdateBlog(blog.ToModel());

        if (!result)
        {
            return new ResponseBaseDTO<ErrorRespDTO>(new ErrorRespDTO(AppMessages.E1004), 403);
        }

        return new ResponseBaseDTO(stat: 200);
    }

    public async Task<ResponseBaseDTO> DeleteBlog(string id)
    {
        var exsBlog = repository.GetBlogById(id);

        if(!validator.ValidateBeforeDeleteBlog(exsBlog))
        {
            return new ResponseBaseDTO<ErrorRespDTO>(new ErrorRespDTO(AppMessages.E1002), 403);
        }

        string fileName = Path.GetFileName(exsBlog.CoverImage);
        var resultDel = handler.DeleteImgFile(exsBlog.AuthorId, fileName, "blogs");
        if(!resultDel)
        {
            return new ResponseBaseDTO<ErrorRespDTO>(new ErrorRespDTO(AppMessages.E1005), 500);
        }

        var result = repository.DeleteBlog(exsBlog);

        if (!result)
        {
            return new ResponseBaseDTO<ErrorRespDTO>(new ErrorRespDTO(AppMessages.E1005), 500);
        }

        return new ResponseBaseDTO(200);
    }

    public ResponseBaseDTO Get5LatestBlogs()
    {
       List<Blog> result = repository.GetAllBlogs()
                                .Take(5)
                                .ToList(); 

        return new ResponseBaseDTO<List<BlogDTO>>(result.ToDTOList(), 200);
    }

    public ResponseBaseDTO GetTop5Blogs()
    {
        List<Blog> result = repository.GetAllBlogs()
                                .OrderByDescending(b => b.AmountOfAccesses)
                                .Take(5)
                                .ToList();

        

        return new ResponseBaseDTO<List<BlogDTO>>(result.ToDTOList(), 200);
    }

    public ResponseBaseDTO SearchBlogs(SearchBlogReqDTO search)
    {
        (List<Blog> blogs, int totalPages) = repository.SearchBlogs(search);


        PageDTO<BlogDTO> blogPage = new PageDTO<BlogDTO>();
        blogPage.CurPage = (search.CurPage > totalPages) ? totalPages : search.CurPage;
        blogPage.PageSize = totalPages;
        blogPage.Datas = blogs.ToDTOList();
        
        return new ResponseBaseDTO<PageDTO<BlogDTO>>(blogPage, 200);
    }

    public ResponseBaseDTO GetBlogDetails(string id, string? username)
    {
        Blog? blog = repository.GetBlogById(id);

        if (blog == null || blog.State == BlogState.DELETED)
        {
            return new ResponseBaseDTO<ErrorRespDTO>(new ErrorRespDTO(AppMessages.E1001), 404);
        }

        var blogDTO = blog.ToDTO();

        if (username != null)
        {
            bool like = blog.Likes?.FirstOrDefault(bl => bl.AuthorId == username)?.LikeOrNot ?? false;
            blogDTO.LikedByUser = like;
        }

        return new ResponseBaseDTO<BlogDTO>(blogDTO, 200);
    }
}