namespace EduFlowHub.Application.Abstractions.Services;

public interface IFileStorage
{
    Task<string> UploadAsync(Stream file, string extension);
    Task<Stream> DownloadAsync(string filePath);
    Task<bool> DeleteAsync(string filePath);
}