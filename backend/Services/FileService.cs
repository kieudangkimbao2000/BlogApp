using BlogApp.Interfaces;
using Serilog;

namespace BlogApp.Services;

public class FileService(ILogger<FileService> logger) : IFileService
{
    public async Task<byte[]> GetCoverImg(string username, string fileName)
    {
        byte[] fileBin = [];
        try
        {
            string filePath = Path.Combine("datas", username, "blogs", fileName);
            if(!File.Exists(filePath))
            {
                logger.LogError($"Can not find ${filePath}");
                return [];
            }

            fileBin = await File.ReadAllBytesAsync(filePath);
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "");
            return [];
        }

        return fileBin;
    }
}