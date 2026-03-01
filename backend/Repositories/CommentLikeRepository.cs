namespace BlogApp.Repositories;

using BlogApp.Dbs;
using BlogApp.Entities;
using BlogApp.Interfaces;

public class CommentLikeRepository(BlogAppContext context, ILogger<CommentLikeRepository> logger) : ICommentLikeRepository
{
    public int CountNumberOfLikes(string commentId)
    {
        return context.CommentLikes.Count(cl => cl.CommentId == commentId);
    }

    public bool AddLike(CommentLike like)
    {
        try
        {
            context.CommentLikes.Add(like);
            context.SaveChanges();

            return true;
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Error adding like for comment with id {CommentId}", like.CommentId);
            return false;
        }
    }

    public bool UpdateLike(CommentLike like)
    {
        try
        {
            context.CommentLikes.Update(like);
            context.SaveChanges();

            return true;
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Error updating like for comment with id {CommentId}", like.CommentId);
            return false;
        }
    }

    public bool DeleteLike(CommentLike like)
    {
        try
        {
            context.CommentLikes.Remove(like);
            context.SaveChanges();

            return true;
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Error deleting like for comment with id {CommentId}", like.CommentId);
            return false;
        }
    }
}