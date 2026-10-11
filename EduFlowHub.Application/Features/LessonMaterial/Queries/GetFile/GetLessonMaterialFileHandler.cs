using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Abstractions.Services.File;
using EduFlowHub.Application.Features.LessonMaterial.Access.Service;
using EduFlowHub.Application.Features.LessonMaterial.Dtos;

namespace EduFlowHub.Application.Features.LessonMaterial.Queries.GetFile;

public class GetLessonMaterialFileHandler
    : IRequestHandler<GetLessonMaterialFileQuery, FileDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorage _fileStorage;
    private readonly ILessonMaterialAccessService _lessonMaterialAccessService;

    public GetLessonMaterialFileHandler(
        IUnitOfWork unitOfWork,
        IFileStorage fileStorage,
        ILessonMaterialAccessService lessonMaterialAccessService)
    {
        _unitOfWork = unitOfWork;
        _fileStorage =  fileStorage;
        _lessonMaterialAccessService = lessonMaterialAccessService;
    }

    public async Task<FileDto> Handle(
        GetLessonMaterialFileQuery request,
        CancellationToken cancellationToken)
    {
        await _lessonMaterialAccessService.EnsureCanViewAsync(request.CourseId, cancellationToken);
        
        Domain.Entities.LessonMaterial? lessonMaterial = await _unitOfWork
            .LessonMaterialRepository
            .GetByIdAsync(
                request.LessonMaterialId,
                cancellationToken,
                lessonMaterial => lessonMaterial.FileMetadata!);
        
        if (lessonMaterial is null)
        {
            throw new EntityNotFoundException(
                "LessonMaterial",
                "Id",
                request.LessonMaterialId.ToString());
        }

        Stream file = await _fileStorage.DownloadAsync(lessonMaterial.FileMetadata!.Path, cancellationToken);
        string fileExtension = lessonMaterial.FileMetadata!.Extension;
        string fileName = $"{request.LessonMaterialId}{fileExtension}";
        
        return new FileDto(file, fileName, fileExtension);
    }
}