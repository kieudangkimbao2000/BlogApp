using BlogApp.Interfaces;

using BlogApp.DTOs;


/// <summary>
///   Implement business logic related to categories
/// </summary>
public interface ICategoryService
{
    /// <summary>
    ///   Get all categories in the database
    /// </summary>
    /// <returns>List of Categories</returns>
    RespDTO GetAllCategories();

    /// <summary>
    ///     Get top 5 categories
    /// </summary>
    /// <returns>List of 5 most popular categories</returns>
    RespDTO GetTop5Categories();

    /// <summary>
    ///     Get a category by id
    /// </summary>
    /// <param name="id">Category's id</param>
    /// <param name="errCode">Error code be returned</param>
    /// <returns>CategoryDTO if found, otherwise null</returns>
    CategoryDTO? GetCategoryById(string id, ref string errCode);

    /// <summary>
    ///   Add a new category to the database
    /// </summary>
    /// <param name="categoryDTO">The category to be added</param>
    /// <param name="errCode">Error code be returned</param>
    /// <returns>True if successful, otherwise false</returns>
    bool AddCategory(CategoryDTO categoryDTO, ref string errCode);

    /// <summary>
    ///    Update an existing category in the database
    /// </summary>
    /// <param name="categoryDTO">The category to be updated</param>
    /// <param name="errCode">Error code be returned</param>
    /// <returns>True if successful, otherwise false</returns>
    bool UpdateCategory(CategoryDTO categoryDTO, ref string errCode);

    /// <summary>
    ///   Delete a category from the database
    /// </summary>
    /// <param name="id">Category's id</param>
    /// <param name="errCode">Error code be returned</param>
    /// <returns>True if successful, otherwise false</returns>
    bool DeleteCategory(string id, ref string errCode);
}