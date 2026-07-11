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
    public async Task<bool> SaveImgFile(string username, string fileName,string desFolder , String base64String)
    {
        try
        {
            // Accept both raw base64 and data URL format: data:image/jpeg;base64,...
            int commaIndex = base64String.IndexOf(',');
            string rawBase64 = commaIndex >= 0 ? base64String[(commaIndex + 1)..] : base64String;

            string folderPath = Path.Combine("./datas", username, desFolder);
            string filePath = Path.Combine(folderPath, fileName);

            if(!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            using var stream = new FileStream(filePath, FileMode.Create);

            byte[] bytes = Convert.FromBase64String(rawBase64);
            await stream.WriteAsync(bytes, 0, bytes.Length);
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
            string filePath = Path.Combine("datas", username, desFolder, fileName);
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