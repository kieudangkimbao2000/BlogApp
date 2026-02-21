namespace BlogApp.Repositories;

using BlogApp.Dbs;
using BlogApp.Entities;
using BlogApp.Interfaces;

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

    public Category? GetCategoryById(string id)
    {
        return context.Categories.FirstOrDefault(c => c.Id == id);
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
            logger.LogError(ex, "Error adding category with id {CategoryId}", category.Id);
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
            logger.LogError(ex, "Error updating category with id {CategoryId}", category.Id);
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
            logger.LogError(ex, "Error deleting category with id {CategoryId}", category.Id);
            return false;
        }
    }
}