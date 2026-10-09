using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Abstractions.Services;
using EduFlowHub.Application.Features.LessonMaterial.Dtos;

namespace EduFlowHub.Application.Features.LessonMaterial.Queries.GetFile;

public class GetLessonMaterialFileHandler
    : IRequestHandler<GetLessonMaterialFileQuery, FileDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorage _fileStorage;

    public GetLessonMaterialFileHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IFileStorage fileStorage)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _fileStorage =  fileStorage;
    }

    public async Task<FileDto> Handle(
        GetLessonMaterialFileQuery request,
        CancellationToken cancellationToken)
    {
        Domain.Entities.LessonMaterial? lessonMaterial = await _unitOfWork
            .LessonMaterialRepository
            .GetByIdAsync(
                request.LessonMaterialId,
                lessonMaterial => lessonMaterial.FileMetadata!);
        
        if (lessonMaterial is null)
        {
            throw new EntityNotFoundException(
                "LessonMaterial",
                "Id",
                request.LessonMaterialId.ToString());
        }

        Stream file = await _fileStorage.DownloadAsync(lessonMaterial.FileMetadata!.Path);
        
        return new FileDto(file);
    }
}