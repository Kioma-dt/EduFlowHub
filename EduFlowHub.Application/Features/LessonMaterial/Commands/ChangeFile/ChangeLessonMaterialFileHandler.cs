using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Abstractions.Services.File;
using EduFlowHub.Application.Features.LessonMaterial.Access.Service;
using EduFlowHub.Application.Features.LessonMaterial.Exceptions;
using EduFlowHub.Domain.Exceptions;

namespace EduFlowHub.Application.Features.LessonMaterial.Commands.ChangeFile;

public class ChangeLessonMaterialFileHandler
    : IRequestHandler<ChangeLessonMaterialFileCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorage _fileStorage;
    private readonly IFileContentTypeChecker  _fileContentTypeChecker;
    private readonly ISupportedFileTypeChecker _supportedFileTypeChecker;
    private readonly IFileStorageRequirements  _fileStorageRequirements;
    private readonly ILessonMaterialAccessService _lessonMaterialAccessService;
    

    public ChangeLessonMaterialFileHandler(
        IUnitOfWork unitOfWork,
        IFileStorage fileStorage,
        IFileContentTypeChecker fileContentTypeChecker,
        ISupportedFileTypeChecker supportedFileTypeChecker,
        IFileStorageRequirements fileStorageRequirements,
        ILessonMaterialAccessService lessonMaterialAccessService)
    {
        _unitOfWork = unitOfWork;
        _fileStorage = fileStorage;
        _fileContentTypeChecker = fileContentTypeChecker;
        _supportedFileTypeChecker = supportedFileTypeChecker;
        _fileStorageRequirements = fileStorageRequirements;
        _lessonMaterialAccessService = lessonMaterialAccessService;
    }

    public async Task Handle(
        ChangeLessonMaterialFileCommand request, 
        CancellationToken cancellationToken)
    {
        Domain.Entities.LessonMaterial? lessonMaterial = await _unitOfWork
            .LessonMaterialRepository
            .GetByIdAsync(request.LessonMaterialId,
                cancellationToken,
                lessonMaterial => lessonMaterial.FileMetadata!,
                lessonMaterial => lessonMaterial.Course!);

        if (lessonMaterial is null)
        {
            throw new EntityNotFoundException(
                "LessonMaterial",
                "Id",
                request.LessonMaterialId.ToString());
        }
        
        _lessonMaterialAccessService.EnsureCanChangeFile(lessonMaterial);

        if (lessonMaterial.Course!.Status != CourseStatus.Draft)
        {
            throw new DomainWrongStatusException("Can not change not draft course");
        }
        
        if (!_fileContentTypeChecker.IsContentTypeCorrect(
                request.FileStream,
                request.FileExtension))
        {
            throw new FileContentTypeMismatchException();
        }

        if (!_supportedFileTypeChecker.IsFileTypeSupported(request.FileExtension))
        {
            throw new UnsupportedFileTypeException(request.FileExtension);
        }

        if (request.FileLength > _fileStorageRequirements.MaxFileSize)
        {
            throw new FileTooLargeException(_fileStorageRequirements.MaxFileSize);
        }
        
        string filePathOld = lessonMaterial.FileMetadata!.Path;

        long fileSize = request.FileStream.Length;
        string filePath = await _fileStorage.UploadAsync(
            request.FileStream,
            request.FileExtension,
            cancellationToken);

        FileMetadata fileMetadata = new FileMetadata(
            filePath,
            request.FileExtension,
            fileSize);
        
        MaterialType materialType = MaterialTypeResolver.FromExtension(request.FileExtension);

        lessonMaterial.ChangeFile(fileMetadata, materialType);

        await _unitOfWork
            .LessonMaterialRepository
            .UpdateAsync(lessonMaterial, cancellationToken);
        
        await _unitOfWork.SaveAsync(cancellationToken);
        
        await _fileStorage.DeleteAsync(filePathOld, cancellationToken);
    }
}