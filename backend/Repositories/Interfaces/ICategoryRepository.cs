namespace BlogApp.Interfaces;

using BlogApp.Entities;

/// <summary>
///     Interact with Categories in the database
/// </summary>
public interface ICategoryRepository
{
    /// <summary>
    ///    Get all categories
    /// </summary>
    /// <returns>List of all categories ordered by name ascending</returns>
    List<Category> GetAllCategories();

    /// <summary>
    ///     Get top 5 categories
    /// </summary>
    /// <returns></returns>
    List<Category> GetTop5Categories();

    /// <summary>
    ///     Get a category by name
    /// </summary>
    /// <param name="name">Category's name</param>
    Category? GetCategoryByName(string name);

    /// <summary>
    ///    Add a category
    /// </summary>
    /// <param name="category">Category data</param>
    /// <returns>True if successfully, otherwise false</returns>
    bool AddCategory(Category category);

    /// <summary>
    ///     Update a category
    /// </summary>
    /// <param name="category">Category data</param>
    /// <returns>True if successfully, otherwise false</returns>
    bool UpdateCategory(Category category);

    /// <summary>
    ///    Delete a category
    /// </summary>
    /// <param name="category">Category data</param>
    /// <returns>True if successfully, otherwise false</returns>
    bool DeleteCategory(Category category);
}