using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Abstractions.Services;
using EduFlowHub.Application.Features.LessonMaterial.Dtos;
using EduFlowHub.Application.Features.LessonMaterial.Exceptions;

namespace EduFlowHub.Application.Features.LessonMaterial.Commands.Create;

public class CreateLessonMaterialHandler
    : IRequestHandler<CreateLessonMaterialCommand, LessonMaterialDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorage _fileStorage;
    private readonly IFileContentTypeChecker  _fileContentTypeChecker;
    private readonly ISupportedFileTypeChecker _supportedFileTypeChecker;

    public CreateLessonMaterialHandler(
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
        CreateLessonMaterialCommand request, 
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
        
        Domain.Entities.Course? course = await _unitOfWork
            .CourseRepository
            .GetByIdAsync(request.CourseId);

        if (course is null)
        {
            throw new InvalidEntityReferenceException(
                "Course",
                request.CourseId);
        }

        Domain.Entities.LessonMaterial? dbLessonMaterial = await _unitOfWork
            .LessonMaterialRepository
            .GetByCourseAndOrderNumberAsync(
                request.CourseId,
                request.OrderNumber);

        if (dbLessonMaterial is not null)
        {
            throw new EntityAlreadyExistsException(
                "LessonMaterial",
                "CourseId/OrderNumber",
                $"{request.CourseId}/{request.OrderNumber}");
        }

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
            .InsertAsync(fileMetadata);
        
        MaterialType materialType = MaterialTypeResolver.FromExtension(request.FileExtension);

        Domain.Entities.LessonMaterial lessonMaterial = new Domain.Entities.LessonMaterial(
            request.Title,
            request.OrderNumber,
            materialType,
            request.CourseId,
            fileMetadata.Id);
        
        await _unitOfWork
            .LessonMaterialRepository
            .InsertAsync(lessonMaterial);
        
        await _unitOfWork.SaveAsync();

        Domain.Entities.LessonMaterial lessonMaterialWithNavigations = await _unitOfWork
            .LessonMaterialRepository
            .GetByIdAsync(
                lessonMaterial.Id,
                lessonMaterial => lessonMaterial.FileMetadata);
        
        return _mapper.Map<LessonMaterialDto>(lessonMaterialWithNavigations);
    }
}