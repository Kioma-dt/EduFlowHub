using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.Course.Dtos;
using EduFlowHub.Application.Filters.Course;

namespace EduFlowHub.Application.Features.Course.Queries.ListPublished;

public class ListPublishedCoursesHandler
    : IRequestHandler<ListPublishedCoursesQuery, IEnumerable<CourseDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseFilterBuilder _filterBuilder;

    public ListPublishedCoursesHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        ICourseFilterBuilder filterBuilder)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _filterBuilder = filterBuilder;
    }
    
    public async Task<IEnumerable<CourseDto>> Handle(
        ListPublishedCoursesQuery request, 
        CancellationToken cancellationToken)
    {
        List<CourseFilter> filters = new List<CourseFilter>()
        {
            new CourseFilter(CourseFilterType.Status, CourseStatus.Published),
        };
        
        if (request.CategoryNormalizedName is not null)
        {
            filters.Add(new CourseFilter(CourseFilterType.CategoryNormalizedName, request.CategoryNormalizedName));
        }
        if(request.AuthorId is not null)
        {
            filters.Add(new CourseFilter(CourseFilterType.AuthorId, request.AuthorId));
        }
        
        IEnumerable<Domain.Entities.Course> courses = await _unitOfWork
            .CourseRepository
            .GetAllAsync(
                _filterBuilder.BuildFilter(filters),
                null,
                cancellationToken,
                course => course.Author!,
                course => course.Category!);

        return _mapper.Map<List<CourseDto>>(courses);
    }
}