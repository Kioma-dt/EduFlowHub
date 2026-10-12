using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.Dtos;
using EduFlowHub.Application.Features.LessonMaterial.Access.Service;
using EduFlowHub.Application.Features.LessonMaterial.Dtos;

namespace EduFlowHub.Application.Features.LessonMaterial.Queries.ListOfCourse;

public class ListLessonMaterialsOfCourseHandler
    : IRequestHandler<ListLessonMaterialsOfCourseQuery, PagedDto<LessonMaterialDto>>
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

    public async Task<PagedDto<LessonMaterialDto>> Handle(
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
                request.Page,
                request.PageSize,
                cancellationToken,
                material => material.FileMetadata!);
        
        var items = _mapper.Map<IEnumerable<LessonMaterialDto>>(lessonMaterials);
        int totalCount =  await _unitOfWork
            .LessonMaterialRepository
            .CountAsync(lessonMaterial => lessonMaterial.CourseId.Equals(request.CourseId), cancellationToken);
        
        return new PagedDto<LessonMaterialDto>(items, request.Page, request.PageSize, totalCount);
    }
}