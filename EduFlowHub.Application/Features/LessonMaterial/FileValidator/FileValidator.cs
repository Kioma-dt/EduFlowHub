using EduFlowHub.Application.Abstractions.Services.File;
using EduFlowHub.Application.Features.LessonMaterial.Exceptions;

namespace EduFlowHub.Application.Features.LessonMaterial.FileValidator;

public class FileValidator
    : IFileValidator
{
    private readonly IFileContentTypeChecker  _fileContentTypeChecker;
    private readonly ISupportedFileTypeChecker _supportedFileTypeChecker;
    private readonly IFileStorageRequirements  _fileStorageRequirements;

    public FileValidator(
        IFileContentTypeChecker fileContentTypeChecker,
        ISupportedFileTypeChecker supportedFileTypeChecker,
        IFileStorageRequirements fileStorageRequirements)
    {
        _fileContentTypeChecker = fileContentTypeChecker;
        _supportedFileTypeChecker = supportedFileTypeChecker;
        _fileStorageRequirements = fileStorageRequirements;
    }

    public void ValidateFile(Stream fileStream, string fileExtension, long fileLength)
    {
        if (!_fileContentTypeChecker.IsContentTypeCorrect(fileStream, fileExtension))
        {
            throw new FileContentTypeMismatchException();
        }

        if (!_supportedFileTypeChecker.IsFileTypeSupported(fileExtension))
        {
            throw new UnsupportedFileExtensionException(fileExtension);
        }

        if (fileLength > _fileStorageRequirements.MaxFileSize)
        {
            throw new FileTooLargeException(_fileStorageRequirements.MaxFileSize);
        }
    }
}