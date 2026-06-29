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
}