namespace BlogApp.Mappers;

using BlogApp.Entities;
using BlogApp.DTOs;

public static class BlogMapper
{
    /// <summary>
    ///     Map Blog Entity to Blog DTO
    /// </summary>
    /// <param name="blog">Blog Entity Object</param>
    /// <returns>Blog DTO Object</returns>
    public static BlogDTO ToDTO(this Blog blog)
    {
        return new BlogDTO
        {
            Id = blog.Id,
            Title = blog.Title,
            Content = blog.Content,
            CoverPhoto = blog.CoverPhoto,
            Categories = blog.Categories,
            State = blog.State,
            AmountOfAccesses = blog.AmountOfAccesses,
            PublishedAt = blog.PublishedAt,
            AuthorId = blog.AuthorId,
            AuthorName = blog.Author != null ? blog.Author.Name : "",
            CreatedAt = blog.CreatedAt,
            UpdatedAt = blog.UpdatedAt
        };
    }

    /// <summary>
    ///     Map Blog DTO to Blog Entity
    /// </summary>
    /// <param name="blogDTO">Blog DTO Object</param>
    /// <returns>Blog Entity Object</returns>
    public static Blog ToModel(this BlogDTO blogDTO)
    {
        return new Blog
        {
            Id = blogDTO.Id,
            Title = blogDTO.Title,
            Content = blogDTO.Content,
            CoverPhoto = blogDTO.CoverPhoto,
            Categories = blogDTO.Categories,
            State = blogDTO.State,
            AmountOfAccesses = blogDTO.AmountOfAccesses,
            AuthorId = blogDTO.AuthorId
        };
    }

    /// <summary>
    ///     Map List of Blog Entity to List of Blog DTO
    /// </summary>
    /// <param name="blogs">List of Blog Entities</param>
    /// <returns>List of Blog DTOs</returns>
    public static List<BlogDTO> ToDTOList(this List<Blog> blogs)
    {
        return blogs.Select(b => b.ToDTO()).ToList();
    }
}