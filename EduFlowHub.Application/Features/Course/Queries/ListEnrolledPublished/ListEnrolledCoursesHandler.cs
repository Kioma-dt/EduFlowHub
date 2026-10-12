using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.Course.Dtos;
using EduFlowHub.Application.Features.Dtos;
using EduFlowHub.Application.Filters.Course;

namespace EduFlowHub.Application.Features.Course.Queries.ListEnrolledPublished;

public class ListEnrolledCoursesPublishedHandler
    : IRequestHandler<ListEnrolledCoursesPublishedQuery, PagedDto<CourseDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseFilterBuilder _filterBuilder;

    public ListEnrolledCoursesPublishedHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        ICourseFilterBuilder filterBuilder)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _filterBuilder = filterBuilder;
    }
    
    public async Task<PagedDto<CourseDto>> Handle(
        ListEnrolledCoursesPublishedQuery request, 
        CancellationToken cancellationToken)
    {
        List<CourseFilter> filters = new List<CourseFilter>()
        {
            new CourseFilter(CourseFilterType.Status, CourseStatus.Published)
        };
        
        if (request.CategoryNormalizedName is not null)
        {
            filters.Add(new CourseFilter(CourseFilterType.CategoryNormalizedName, request.CategoryNormalizedName));
        }
        if(request.AuthorId is not null)
        {
            filters.Add(new CourseFilter(CourseFilterType.AuthorId, request.AuthorId));
        }
        
        var expression = _filterBuilder.BuildFilter(filters);
        
        IEnumerable<Domain.Entities.Course> courses = await _unitOfWork
            .CourseRepository
            .GetEnrolledCoursesAsync(
                request.StudentId, 
                expression,
                null,
                request.Page,
                request.PageSize,
                cancellationToken);

        var items = _mapper.Map<List<CourseDto>>(courses);
        int totalCount = await _unitOfWork
            .CourseRepository
            .CountAsync(expression, cancellationToken);
        
        return new PagedDto<CourseDto>(items, request.Page, request.PageSize, totalCount);
    }
}