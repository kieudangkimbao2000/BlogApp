namespace BlogApp.Repositories;

using BlogApp.Dbs;
using BlogApp.Entities;
using BlogApp.Interfaces;
using Microsoft.EntityFrameworkCore;

/// <summary>
///   Implement Tag Repository Interface
/// </summary>
/// <param name="context"></param>
/// <param name="logger"></param>
public class TagRepository(BlogAppContext context, 
                                ILogger<TagRepository> logger) : ITagRepository
{
    public List<Tag> GetAllTags()
    {
        return context.Tags.OrderByDescending(t => t.Name).ToList();
    }

    public List<Tag> GetTop5Tags()
    {
        var result = context.Database.SqlQueryRaw<string>(@"
            SELECT tag AS Name, COUNT(*) AS blog_count
            FROM ""Blogs"", UNNEST(""Tags"") AS tag
            GROUP BY tag
            ORDER BY blog_count DESC
            LIMIT 5
        ").ToList();
        
        List<Tag> tags = new List<Tag>();

        foreach(var tagName in result)
        {
            Tag tag = new Tag();
            tag.Name = tagName;
            tags.Add(tag);
        }

        return tags;
    }

    public Tag? GetTagByName(string name)
    {
        return context.Tags.FirstOrDefault(t => t.Name == name);
    }

    public bool AddTag(Tag tag)
    {
        try
        {
            context.Tags.Add(tag);
            context.SaveChanges();

            return true;
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Error adding tag with name {TagName}", tag.Name);
            return false;
        }
    }

    public bool UpdateTag(Tag tag)
    {
        try
        {
            context.Tags.Update(tag);
            context.SaveChanges();

            return true;
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Error updating tag with name {TagName}", tag.Name);
            return false;
        }
    }

    public bool DeleteTag(Tag tag)
    {
        try
        {
            context.Tags.Remove(tag);
            context.SaveChanges();

            return true;
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Error deleting tag with name {TagName}", tag.Name);
            return false;
        }
    }
}