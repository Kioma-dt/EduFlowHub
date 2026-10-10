using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.LessonMaterial.Dtos;

namespace EduFlowHub.Application.Features.LessonMaterial.Queries.GetByOrderInCourse;

public class GetLessonMaterialByOrderInCourseHandler
    : IRequestHandler<GetLessonMaterialByOrderInCourseQuery, LessonMaterialDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetLessonMaterialByOrderInCourseHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<LessonMaterialDto> Handle(
        GetLessonMaterialByOrderInCourseQuery request,
        CancellationToken cancellationToken)
    {
        Domain.Entities.LessonMaterial? lessonMaterial = await _unitOfWork
            .LessonMaterialRepository
            .GetByCourseAndOrderNumberAsync(
                request.CourseId,
                request.OrderNumber!,
                cancellationToken);
        
        if (lessonMaterial is null)
        {
            throw new EntityNotFoundException(
                "LessonMaterial",
                "CourseId/OrderNumber",
                $"{request.CourseId}/{request.OrderNumber}");
        }
        
        return _mapper.Map<LessonMaterialDto>(lessonMaterial);
    }
}