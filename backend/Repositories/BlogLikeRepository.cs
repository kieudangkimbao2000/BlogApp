namespace BlogApp.Repositories;

using BlogApp.Dbs;
using BlogApp.Entities;
using BlogApp.Interfaces;

/// <summary>
///     Implement BlogLike Repository Interface
/// </summary>
/// <param name="context"></param>
/// <param name="logger"></param>
public class BlogLikeRepository(List<DTOs.BlogDTO> blogDTOs, BlogAppContext context, ILogger<BlogLikeRepository> logger) : IBlogLikeRepository
{
    public int CountNumberOfLikes(string blogId)
    {
        return context.BlogLikes.Count(bl => bl.BlogId == blogId);
    }

    public bool AddLike(BlogLike like)
    {
        try
        {
            context.BlogLikes.Add(like);
            context.SaveChanges();

            return true;
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Error adding like for blog with id {BlogId} by user {AuthorId}", like.BlogId, like.AuthorId);
            return false;
        }
    }

    public bool UpdateLike(BlogLike like)
    {
        try
        {
            context.BlogLikes.Update(like);
            context.SaveChanges();

            return true;
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Error updating like for blog with id {BlogId} by user {AuthorId}", like.BlogId, like.AuthorId);
            return false;
        }
    }

    public bool DeleteLike(BlogLike like)
    {
        try
        {
            context.BlogLikes.Remove(like);
            context.SaveChanges();

            return true;
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Error deleting like for blog with id {BlogId} by user {AuthorId}", like.BlogId, like.AuthorId);
            return false;
        }
    }
}