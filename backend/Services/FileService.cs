using BlogApp.Interfaces;
using Serilog;

namespace BlogApp.Services;

public class FileService(ILogger<FileService> logger) : IFileService
{
    public async Task<byte[]> GetCoverImg(string username, string fileName)
    {
        byte[] fileBin = Array.Empty<byte>();
        try
        {
            string filePath = Path.Combine("datas", username, "blogs", fileName);
            if(!File.Exists(filePath))
            {
                logger.LogError($"Can not find {filePath}");
                return Array.Empty<byte>();
            }

            fileBin = await File.ReadAllBytesAsync(filePath);
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "");
            return Array.Empty<byte>();
        }

        return fileBin;
    }

    public async Task<byte[]> GetProfileImg(string username, string fileName)
    {
        byte[] fileBin = Array.Empty<byte>();
        try
        {
            string filePath = Path.Combine("datas", username, "profile", fileName);
            if(!File.Exists(filePath))
            {
                logger.LogError($"Can not find {filePath}");
                return Array.Empty<byte>();
            }

            fileBin = await File.ReadAllBytesAsync(filePath);
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "");
            return Array.Empty<byte>();
        }

        return fileBin;
    }
}