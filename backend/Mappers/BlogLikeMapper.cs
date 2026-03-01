namespace BlogApp.Mappers;

using BlogApp.DTOs;
using BlogApp.Entities;

/// <summary>
///    Map between BlogLike Entity and DTO
/// </summary>
public static class BlogLikeMapper
{
    /// <summary>
    ///    Map BlogLike Entity to BlogLike DTO
    /// </summary>
    /// <param name="blogLike">BlogLike Entity Object</param>
    /// <returns>BlogLike DTO Object</returns>
    public static BlogLikeDTO ToDTO(this BlogLike blogLike)
    {
        return new BlogLikeDTO
        {
            AuthorId = blogLike.AuthorId,
            BlogId = blogLike.BlogId,
            LikeOrDislike = blogLike.LikeOrDislike
        };
    }

    /// <summary>
    ///   Map BlogLike DTO to BlogLike Entity
    /// </summary>
    /// <param name="blogLikeDTO">BlogLike DTO Object</param>
    /// <returns>BlogLike Entity Object</returns>
    public static BlogLike ToModel(this BlogLikeDTO blogLikeDTO)
    {
        return new BlogLike
        {
            AuthorId = blogLikeDTO.AuthorId,
            BlogId = blogLikeDTO.BlogId,
            LikeOrDislike = blogLikeDTO.LikeOrDislike
        };
    }

    /// <summary>
    ///    Map List of BlogLike Entity to List of BlogLike DTO
    /// </summary>
    /// <param name="blogLikes">List of BlogLike Entities</param>
    /// <returns>List of BlogLike DTOs</returns>
    public static List<BlogLikeDTO> ToDTOList(this List<BlogLike> blogLikes)
    {
        return blogLikes.Select(bl => bl.ToDTO()).ToList();
    }
}