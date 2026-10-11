using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.Course.Dtos;
using EduFlowHub.Application.Filters.Course;

namespace EduFlowHub.Application.Features.Course.Queries.ListEnrolledPublished;

public class ListEnrolledCoursesPublishedHandler
    : IRequestHandler<ListEnrolledCoursesPublishedQuery, IEnumerable<CourseDto>>
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
    
    public async Task<IEnumerable<CourseDto>> Handle(
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
        
        IEnumerable<Domain.Entities.Course> courses = await _unitOfWork
            .CourseRepository
            .GetEnrolledCoursesAsync(
                request.StudentId, 
                _filterBuilder.BuildFilter(filters),
                cancellationToken);
        
        return _mapper.Map<List<CourseDto>>(courses);
    }
}