namespace BlogApp.Repositories;

using BlogApp.Dbs;
using BlogApp.Entities;
using BlogApp.Interfaces;
using Microsoft.EntityFrameworkCore;

/// <summary>
///   Implement Category Repository Interface
/// </summary>
/// <param name="context"></param>
/// <param name="logger"></param>
public class CategoryRepository(BlogAppContext context, 
                                ILogger<CategoryRepository> logger) : ICategoryRepository
{
    public List<Category> GetAllCategories()
    {
        return context.Categories.OrderByDescending(c => c.Name).ToList();
    }

    public List<Category> GetTop5Categories()
    {
        var result = context.Database.SqlQueryRaw<string>(@"
            SELECT cat AS Name, COUNT(*) AS blog_count
            FROM ""Blogs"", UNNEST(""Categories"") AS cat
            GROUP BY cat
            ORDER BY blog_count DESC
            LIMIT 5
        ").ToList();
        
        List<Category> categs = new List<Category>();

        foreach(var categName in result)
        {
            Category categ = new Category();
            categ.Name = categName;
            categs.Add(categ);
        }

        return categs;
    }

    public Category? GetCategoryByName(string name)
    {
        return context.Categories.FirstOrDefault(c => c.Name == name);
    }

    public bool AddCategory(Category category)
    {
        try
        {
            context.Categories.Add(category);
            context.SaveChanges();

            return true;
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Error adding category with name {CategoryName}", category.Name);
            return false;
        }
    }

    public bool UpdateCategory(Category category)
    {
        try
        {
            context.Categories.Update(category);
            context.SaveChanges();

            return true;
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Error updating category with name {CategoryName}", category.Name);
            return false;
        }
    }

    public bool DeleteCategory(Category category)
    {
        try
        {
            context.Categories.Remove(category);
            context.SaveChanges();

            return true;
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Error deleting category with name {CategoryName}", category.Name);
            return false;
        }
    }
}