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
    ///     Get a category by id
    /// </summary>
    /// <param name="id">Category's id</param>
    Category? GetCategoryById(string id);

    /// <summary>
    ///    Add a category
    /// </summary>
    /// <param name="category">Category data</param>
    /// <returns>True if added successfully, otherwise false</returns>
    bool AddCategory(Category category);

    /// <summary>
    ///     Update a category
    /// </summary>
    /// <param name="category">Category data</param>
    /// <returns>True if updated successfully, otherwise false</returns>
    bool UpdateCategory(Category category);

    /// <summary>
    ///    Delete a category
    /// </summary>
    /// <param name="category">Category data</param>
    /// <returns>True if deleted successfully, otherwise false</returns>
    bool DeleteCategory(Category category);
}