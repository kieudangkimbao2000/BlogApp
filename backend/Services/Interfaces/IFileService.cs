namespace BlogApp.Interfaces;

public interface IFileService
{
    /// <summary>
    ///     Get blog's cover image
    /// </summary>
    /// <param name="username"></param>
    /// <param name="fileName"></param>
    /// <returns>
    ///     Binary of file
    /// </returns>
    Task<byte[]> GetCoverImg(string username, string fileName);

    /// <summary>
    ///    Get user's profile image
    /// </summary>
    /// <param name="username"></param>
    /// <param name="fileName"></param>
    /// <returns>
    ///     Binary of file
    /// </returns>
    Task<byte[]> GetProfileImg(string username, string fileName);
}