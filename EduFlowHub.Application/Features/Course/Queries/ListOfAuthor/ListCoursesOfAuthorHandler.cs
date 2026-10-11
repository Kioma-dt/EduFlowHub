using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.Course.Dtos;
using EduFlowHub.Application.Filters.Course;

namespace EduFlowHub.Application.Features.Course.Queries.ListOfAuthor;

public class ListCoursesOfAuthorHandler
    : IRequestHandler<ListCoursesOfAuthorQuery, IEnumerable<CourseDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseFilterBuilder _filterBuilder;

    public ListCoursesOfAuthorHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        ICourseFilterBuilder filterBuilder)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _filterBuilder = filterBuilder;
    }
    
    public async Task<IEnumerable<CourseDto>> Handle(
        ListCoursesOfAuthorQuery request, 
        CancellationToken cancellationToken)
    {
        List<CourseFilter> filters = new List<CourseFilter>()
        {
            new CourseFilter(CourseFilterType.AuthorId, request.AuthorId),
        };
        
        if (request.CategoryNormalizedName is not null)
        {
            filters.Add(new CourseFilter(CourseFilterType.CategoryNormalizedName, request.CategoryNormalizedName));
        }
        if(request.Status is not null)
        {
            filters.Add(new CourseFilter(CourseFilterType.Status, request.Status));
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