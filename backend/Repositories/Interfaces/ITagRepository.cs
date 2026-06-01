namespace BlogApp.Interfaces;

using BlogApp.Entities;

/// <summary>
///     Interact with Tags in the database
/// </summary>
public interface ITagRepository
{
    /// <summary>
    ///    Get all tags
    /// </summary>
    /// <returns>List of all tags ordered by name ascending</returns>
    List<Tag> GetAllTags();

    /// <summary>
    ///     Get top 5 tags
    /// </summary>
    /// <returns></returns>
    List<Tag> GetTop5Tags();

    /// <summary>
    ///     Get a tag by name
    /// </summary>
    /// <param name="name">Tag's name</param>
    Tag? GetTagByName(string name);

    /// <summary>
    ///    Add a tag
    /// </summary>
    /// <param name="tag">Tag data</param>
    /// <returns>True if successfully, otherwise false</returns>
    bool AddTag(Tag tag);

    /// <summary>
    ///     Update a tag
    /// </summary>
    /// <param name="tag">Tag data</param>
    /// <returns>True if successfully, otherwise false</returns>
    bool UpdateTag(Tag tag);

    /// <summary>
    ///    Delete a tag
    /// </summary>
    /// <param name="tag">Tag data</param>
    /// <returns>True if successfully, otherwise false</returns>
    bool DeleteTag(Tag tag);
}