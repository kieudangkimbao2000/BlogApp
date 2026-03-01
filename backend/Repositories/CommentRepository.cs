namespace BlogApp.Repositories;

using BlogApp.Dbs;
using BlogApp.Entities;
using BlogApp.Interfaces;

/// <summary>
///     Implement Comment Repository Interface
/// </summary>
/// <param name="context"></param>
/// <param name="logger"></param>
public class CommentRepository(BlogAppContext context, ILogger<CommentRepository> logger) : ICommentRepository
{
    public List<Comment> GetCommentsByBlogId(string blogId)
    {
        return context.Comments.Where(c => c.BlogId == blogId)
                                .OrderByDescending(c => c.CreatedAt)
                                .ToList();
    }

    public bool AddComment(Comment comment)
    {
        try
        {
            context.Comments.Add(comment);
            context.SaveChanges();

            return true;
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Error adding comment with id {CommentId}", comment.Id);
            return false;
        }
    }

    public bool UpdateComment(Comment comment)
    {
        try
        {
            context.Comments.Update(comment);
            context.SaveChanges();

            return true;
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Error updating comment with id {CommentId}", comment.Id);
            return false;
        }
    }

    public bool DeleteComment(Comment comment)
    {
        try
        {
            context.Comments.Remove(comment);
            context.SaveChanges();

            return true;
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Error deleting comment with id {CommentId}", comment.Id);
            return false;
        }
    }
}