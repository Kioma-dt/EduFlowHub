namespace EduFlowHub.Application.Abstractions.Services;

public interface IFileStorage
{
    Task<string> Upload(Stream file, string extension);
    Task<Stream> Download(string filePath);
    Task<bool> Delete(string filePath);
}