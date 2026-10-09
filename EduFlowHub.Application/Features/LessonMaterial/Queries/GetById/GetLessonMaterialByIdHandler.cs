using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.LessonMaterial.Dtos;

namespace EduFlowHub.Application.Features.LessonMaterial.Queries.GetById;

public class GetLessonMaterialByIdHandler
    : IRequestHandler<GetLessonMaterialByIdQuery, LessonMaterialDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetLessonMaterialByIdHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<LessonMaterialDto> Handle(
        GetLessonMaterialByIdQuery request,
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
        
        return _mapper.Map<LessonMaterialDto>(lessonMaterial);
    }
}