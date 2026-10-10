using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Abstractions.Services;
using EduFlowHub.Application.Abstractions.Services.File;
using EduFlowHub.Application.Features.LessonMaterial.Dtos;
using EduFlowHub.Application.Features.LessonMaterial.Exceptions;

namespace EduFlowHub.Application.Features.LessonMaterial.Commands.ChangeFile;

public class ChangeLessonMaterialFileHandler
    : IRequestHandler<ChangeLessonMaterialFileCommand, LessonMaterialDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorage _fileStorage;
    private readonly IFileContentTypeChecker  _fileContentTypeChecker;
    private readonly ISupportedFileTypeChecker _supportedFileTypeChecker;
    private readonly IFileStorageRequirements  _fileStorageRequirements;
    

    public ChangeLessonMaterialFileHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IFileStorage fileStorage,
        IFileContentTypeChecker fileContentTypeChecker,
        ISupportedFileTypeChecker supportedFileTypeChecker,
        IFileStorageRequirements fileStorageRequirements)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _fileStorage = fileStorage;
        _fileContentTypeChecker = fileContentTypeChecker;
        _supportedFileTypeChecker = supportedFileTypeChecker;
        _fileStorageRequirements = fileStorageRequirements;
    }

    public async Task<LessonMaterialDto> Handle(
        ChangeLessonMaterialFileCommand request, 
        CancellationToken cancellationToken)
    {
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

        if (request.FileStream.Length > _fileStorageRequirements.MaxFileSize)
        {
            throw new FileTooLargeException(_fileStorageRequirements.MaxFileSize);
        }
        
        Domain.Entities.LessonMaterial? lessonMaterial = await _unitOfWork
            .LessonMaterialRepository
            .GetByIdAsync(request.LessonMaterialId,
                cancellationToken,
                lessonMaterial => lessonMaterial.FileMetadata!);

        if (lessonMaterial is null)
        {
            throw new EntityNotFoundException(
                "LessonMaterial",
                "Id",
                request.LessonMaterialId.ToString());
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
        
        await _unitOfWork
            .FileMetadataRepository
            .DeleteAsync(lessonMaterial.FileMetadata, cancellationToken);
        
        await _unitOfWork
            .FileMetadataRepository
            .InsertAsync(fileMetadata, cancellationToken);
        
        MaterialType materialType = MaterialTypeResolver.FromExtension(request.FileExtension);

        lessonMaterial.ChangeFile(
            fileMetadata,
            materialType);
        
        await _unitOfWork
            .LessonMaterialRepository
            .UpdateAsync(lessonMaterial, cancellationToken);
        
        await _unitOfWork.SaveAsync(cancellationToken);
        
        await _fileStorage.DeleteAsync(filePathOld, cancellationToken);
        
        return _mapper.Map<LessonMaterialDto>(lessonMaterial);
    }
}