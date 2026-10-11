using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.LessonMaterial.Access.Service;
using EduFlowHub.Application.Features.LessonMaterial.Dtos;

namespace EduFlowHub.Application.Features.LessonMaterial.Queries.ListOfCourse;

public class ListLessonMaterialsOfCourseHandler
    : IRequestHandler<ListLessonMaterialsOfCourseQuery, IEnumerable<LessonMaterialDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILessonMaterialAccessService _lessonMaterialAccessService;

    public ListLessonMaterialsOfCourseHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        ILessonMaterialAccessService lessonMaterialAccessService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _lessonMaterialAccessService = lessonMaterialAccessService;
    }

    public async Task<IEnumerable<LessonMaterialDto>> Handle(
        ListLessonMaterialsOfCourseQuery request,
        CancellationToken cancellationToken)
    {
        await _lessonMaterialAccessService.EnsureCanViewAsync(request.CourseId, cancellationToken);
        
        Func<IQueryable<Domain.Entities.LessonMaterial>, IOrderedQueryable<Domain.Entities.LessonMaterial>>? orderBy= null;
        if (request.OrderByNumber)
        {
            orderBy = lessonMaterials 
                => lessonMaterials
                    .OrderBy(lessonMaterial =>  lessonMaterial.OrderNumber);
        }
        
        IEnumerable<Domain.Entities.LessonMaterial> lessonMaterials = await _unitOfWork
            .LessonMaterialRepository
            .GetAllAsync(
                lessonMaterial => lessonMaterial.CourseId.Equals(request.CourseId),
                orderBy,
                cancellationToken);
        
        return _mapper.Map<IEnumerable<LessonMaterialDto>>(lessonMaterials);
    }
}