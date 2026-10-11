using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.LessonMaterial.Access.Service;
using EduFlowHub.Application.Features.LessonMaterial.Dtos;

namespace EduFlowHub.Application.Features.LessonMaterial.Queries.GetByOrderInCourse;

public class GetLessonMaterialByOrderInCourseHandler
    : IRequestHandler<GetLessonMaterialByOrderInCourseQuery, LessonMaterialDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILessonMaterialAccessService _lessonMaterialAccessService;

    public GetLessonMaterialByOrderInCourseHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        ILessonMaterialAccessService lessonMaterialAccessService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _lessonMaterialAccessService = lessonMaterialAccessService;
    }

    public async Task<LessonMaterialDto> Handle(
        GetLessonMaterialByOrderInCourseQuery request,
        CancellationToken cancellationToken)
    {
        await _lessonMaterialAccessService.EnsureCanViewAsync(request.CourseId, cancellationToken);
        
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