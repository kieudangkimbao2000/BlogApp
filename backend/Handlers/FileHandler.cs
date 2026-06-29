namespace BlogApp.Handlers;


public class FileHandler(ILogger<FileHandler> logger)
{
    /// <summary>
    ///     Savinng image files like blog's cover image, avatar image, ...
    /// </summary>
    /// <param name="username"></param>
    /// <param name="fileName"></param>
    /// <param name="desFolder"></param>
    /// <param name="file"></param>
    /// <returns>True if saving file successfully, else fase</returns>
    public async Task<bool> SaveImgFile(string username, string fileName,string desFolder , IFormFile file)
    {
        try
        {
            string folderPath = Path.Combine("./datas", username, desFolder);
            string filePath = Path.Combine(folderPath, fileName);

            if(Directory.Exists(Directory.GetParent(folderPath).FullName))
            {
                Directory.CreateDirectory(folderPath);
            }

            using var stream = new FileStream(filePath, FileMode.Create);

            await file.CopyToAsync(stream);
        }
        catch(UnauthorizedAccessException ex)
        {
            logger.LogError(ex,"");
            return false;
        }
        catch(Exception ex)
        {
            logger.LogError(ex,"");
            return false;
        }

        return true;
    }

    /// <summary>
    ///     Deleting image files
    /// </summary>
    /// <param name="username"></param>
    /// <param name="fileName"></param>
    /// <param name="desFolder"></param>
    /// <returns>True if deleting file successfully, else False</returns>
    public bool DeleteImgFile(string username, string fileName, string desFolder)
    {
        try{
            string filePath = Path.Combine(username, desFolder, fileName);
            if(!File.Exists(filePath)) return false;

            File.Delete(filePath);
        }
        catch(UnauthorizedAccessException ex)
        {
            logger.LogError(ex, "");
            return false;
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "");
            return false;
        }

        return true;
    }
}