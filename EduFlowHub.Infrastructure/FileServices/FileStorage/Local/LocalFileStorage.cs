using System.Text.RegularExpressions;
using EduFlowHub.Application.Abstractions.Services.File;
using EduFlowHub.Application.Features.LessonMaterial.Exceptions;
using EduFlowHub.Infrastructure.FileServices.Exceptions;
using EduFlowHub.Infrastructure.FileServices.FileStorage.Local.Options;
using Microsoft.Extensions.Options;

namespace EduFlowHub.Infrastructure.FileServices.FileStorage.Local;

public class LocalFileStorage
    : IFileStorage
{
    private static readonly Regex StorageFilePathRegex = new(
        @"^[a-fA-F0-9]{32}\.[a-zA-Z0-9]+$",
        RegexOptions.Compiled | RegexOptions.CultureInvariant);
    
    private static readonly Regex FileExtensionRegex = new(
        @"^\.[a-zA-Z0-9]+$",
        RegexOptions.Compiled | RegexOptions.CultureInvariant);
    
    private readonly string _rootPath;
    private readonly int _maxFileSize;
    private readonly HashSet<string> _supportedExtensions;

    public LocalFileStorage(
        IOptions<LocalRootFileStoragePathOptions> rootFilePathOptions,
        IOptions<LocalMaxFileSizeOptions>  maxFileSizeOptions,
        IOptions<LocalSupportedFileExtensionsOptions> supportedExtensions)
    {
        _rootPath = rootFilePathOptions.Value.RootPath;
        _maxFileSize = maxFileSizeOptions.Value.MaxFileSize;
        _supportedExtensions = new HashSet<string>(supportedExtensions
            .Value
            .Extensions
            .Select(extension => extension.ToLowerInvariant()));
        Directory.CreateDirectory(_rootPath);
    }
    
    public async Task<string> UploadAsync(
        Stream fileStream, 
        string fileExtension)
    {
        ArgumentNullException.ThrowIfNull(fileStream);

        if (fileStream.CanSeek 
            && fileStream.Length == 0)
        {
            throw new EmptyStreamException();
        }
        
        if (!FileExtensionRegex.IsMatch(fileExtension)
            || !_supportedExtensions.Contains(fileExtension.ToLowerInvariant()))
        {
            throw new UnsupportedFileTypeException(fileExtension);
        }
        
        var filePath = $"{Guid.NewGuid():N}{fileExtension.ToLowerInvariant()}";
        string fullPath = GetFullPath(filePath);
        
        await using var destination = new FileStream(
            fullPath,
            FileMode.CreateNew,
            FileAccess.Write,
            FileShare.None,
            bufferSize: _maxFileSize,
            useAsync: true);

        await fileStream.CopyToAsync(destination);

        return filePath;
    }

    public Task<Stream> DownloadAsync(
        string filePath)
    {
        var fullPath = GetFullPath(filePath);

        Stream stream = new FileStream(
            fullPath,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read,
            bufferSize: _maxFileSize,
            useAsync: true);

        return Task.FromResult(stream);
    }

    public Task<bool> DeleteAsync(
        string filePath)
    {
        var fullPath = GetFullPath(filePath);

        if (!File.Exists(fullPath))
        {
            return Task.FromResult(false);
        }

        File.Delete(fullPath);

        return Task.FromResult(true);
    }
    
    private string GetFullPath(string filePath)
    {
        if (!StorageFilePathRegex.IsMatch(filePath))
        {
            throw new InvalidStorageFilePath(filePath);
        }

        var fullPath = Path.GetFullPath(
            Path.Combine(_rootPath, filePath));

        return fullPath;
    }

}