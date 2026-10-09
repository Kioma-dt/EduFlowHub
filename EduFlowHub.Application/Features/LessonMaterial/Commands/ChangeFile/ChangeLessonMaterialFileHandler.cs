using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Abstractions.Services;
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

    public ChangeLessonMaterialFileHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IFileStorage fileStorage,
        IFileContentTypeChecker fileContentTypeChecker,
        ISupportedFileTypeChecker supportedFileTypeChecker)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _fileStorage = fileStorage;
        _fileContentTypeChecker = fileContentTypeChecker;
        _supportedFileTypeChecker = supportedFileTypeChecker;
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
        
        Domain.Entities.LessonMaterial? lessonMaterial = await _unitOfWork
            .LessonMaterialRepository
            .GetByIdAsync(request.LessonMaterialId,
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
            request.FileExtension);

        //! КОСТЫЛЬ
        FileMetadata fileMetadata = new FileMetadata(
            filePath,
            request.FileExtension,
            fileSize);
        fileMetadata.GenerateGuid();
        
        await _unitOfWork
            .FileMetadataRepository
            .DeleteAsync(lessonMaterial.FileMetadata);
        
        await _unitOfWork
            .FileMetadataRepository
            .InsertAsync(fileMetadata);
        
        MaterialType materialType = MaterialTypeResolver.FromExtension(request.FileExtension);

        lessonMaterial.ChangeFile(fileMetadata.Id,
            materialType);
        
        await _unitOfWork
            .LessonMaterialRepository
            .UpdateAsync(lessonMaterial);
        
        await _unitOfWork.SaveAsync();
        
        await _fileStorage.DeleteAsync(filePathOld);
        
        Domain.Entities.LessonMaterial lessonMaterialWithNavigations = await _unitOfWork
            .LessonMaterialRepository
            .GetByIdAsync(
                lessonMaterial.Id,
                lessonMaterial => lessonMaterial.FileMetadata);
        
        return _mapper.Map<LessonMaterialDto>(lessonMaterialWithNavigations);
    }
}