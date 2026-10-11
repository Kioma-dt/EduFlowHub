using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Abstractions.Services.File;
using EduFlowHub.Application.Features.LessonMaterial.Access.Service;
using EduFlowHub.Application.Features.LessonMaterial.Dtos;
using EduFlowHub.Application.Features.LessonMaterial.Exceptions;
using EduFlowHub.Application.Features.LessonMaterial.FileValidator;

namespace EduFlowHub.Application.Features.LessonMaterial.Commands.Create;

public class CreateLessonMaterialHandler
    : IRequestHandler<CreateLessonMaterialCommand, LessonMaterialDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorage _fileStorage;
    private readonly IFileValidator _fileValidator;
    private readonly ILessonMaterialAccessService _lessonMaterialAccessService;

    public CreateLessonMaterialHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IFileStorage fileStorage,
        IFileValidator fileValidator,
        ILessonMaterialAccessService lessonMaterialAccessService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _fileStorage = fileStorage;
        _fileValidator = fileValidator;
        _lessonMaterialAccessService = lessonMaterialAccessService;
    }

    public async Task<LessonMaterialDto> Handle(
        CreateLessonMaterialCommand request, 
        CancellationToken cancellationToken)
    {
        Domain.Entities.Course? course = await _unitOfWork
            .CourseRepository
            .GetByIdAsync(request.CourseId, cancellationToken);

        if (course is null)
        {
            throw new InvalidEntityReferenceException(
                "Course",
                request.CourseId);
        }
        
        _lessonMaterialAccessService.EnsureCanCreate(course.AuthorId);
        
        course.EnsureCanChange();
        
        _fileValidator.ValidateFile(request.FileStream, request.FileExtension, request.FileLength);
        
        if (await _unitOfWork
                .LessonMaterialRepository
                .GetByCourseAndOrderNumberAsync(
                    request.CourseId,
                    request.OrderNumber,
                    cancellationToken) is not null)
        {
            throw new EntityAlreadyExistsException(
                "LessonMaterial",
                "CourseId/OrderNumber",
                $"{request.CourseId}/{request.OrderNumber}");
        }

        long fileSize = request.FileStream.Length;
        string filePath = await _fileStorage.UploadAsync(
            request.FileStream,
            request.FileExtension,
            cancellationToken);

        FileMetadata fileMetadata = new FileMetadata(filePath, request.FileExtension, fileSize);
        
        MaterialType materialType = MaterialTypeResolver.FromExtension(request.FileExtension);

        Domain.Entities.LessonMaterial lessonMaterial = new Domain.Entities.LessonMaterial(
            request.Title,
            request.OrderNumber,
            materialType,
            request.CourseId,
            fileMetadata);
        
        await _unitOfWork
            .LessonMaterialRepository
            .InsertAsync(lessonMaterial, cancellationToken);
        
        await _unitOfWork.SaveAsync(cancellationToken);
        
        var fileMetadataDto = _mapper.Map<FileMetadataDto>(fileMetadata);
        var lessonMaterialDto = new LessonMaterialDto(
            lessonMaterial.Id,
            lessonMaterial.Title,
            lessonMaterial.OrderNumber,
            lessonMaterial.Type,
            lessonMaterial.CreatedAt,
            fileMetadataDto);
        
        return lessonMaterialDto;
    }
}