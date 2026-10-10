namespace EduFlowHub.Application.Abstractions.Services.File;

public interface IFileStorage
{
    Task<string> UploadAsync(Stream file, string extension, CancellationToken cancellationToken = default);
    Task<Stream> DownloadAsync(string filePath, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(string filePath, CancellationToken cancellationToken = default);
}