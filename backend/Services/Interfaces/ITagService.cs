namespace BlogApp.Interfaces;

using BlogApp.DTOs;


/// <summary>
///   Implement business logic related to tags
/// </summary>
public interface ITagService
{
    /// <summary>
    ///   Get all tags in the database
    /// </summary>
    /// <returns>List of Tags</returns>
    RespDTO GetAllTags();

    /// <summary>
    ///     Get top 5 tags
    /// </summary>
    /// <returns>List of 5 most popular tags</returns>
    RespDTO GetTop5Tags();

    /// <summary>
    ///     Get a tag by name
    /// </summary>
    /// <param name="name">Tag's name</param>
    /// <param name="errCode">Error code be returned</param>
    /// <returns>TagDTO if found, otherwise null</returns>
    TagDTO? GetTagByName(string name, ref string errCode);

    /// <summary>
    ///   Add a new tag to the database
    /// </summary>
    /// <param name="tagDTO">The tag to be added</param>
    /// <param name="errCode">Error code be returned</param>
    /// <returns>True if successful, otherwise false</returns>
    bool AddTag(TagDTO tagDTO, ref string errCode);

    /// <summary>
    ///    Update an existing tag in the database
    /// </summary>
    /// <param name="tagDTO">The tag to be updated</param>
    /// <param name="errCode">Error code be returned</param>
    /// <returns>True if successful, otherwise false</returns>
    bool UpdateTag(TagDTO tagDTO, ref string errCode);

    /// <summary>
    ///   Delete a tag from the database
    /// </summary>
    /// <param name="name">Tag's name</param>
    /// <param name="errCode">Error code be returned</param>
    /// <returns>True if successful, otherwise false</returns>
    bool DeleteTag(string name, ref string errCode);
}