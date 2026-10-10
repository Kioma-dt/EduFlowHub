using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.Course.Dtos;

namespace EduFlowHub.Application.Features.Course.Queries.ListPublished;

public class ListPublishedCoursesHandler
    : IRequestHandler<ListPublishedCoursesQuery, IEnumerable<CourseDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ListPublishedCoursesHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<CourseDto>> Handle(
        ListPublishedCoursesQuery request, 
        CancellationToken cancellationToken)
    {
        Domain.Entities.CourseCategory? category = null;

        if (request.Category is not null)
        {
            category = await _unitOfWork
                .CourseCategoryRepository
                .GetByNormalizedNameAsync(request.Category);
        }
        
        var courses = await _unitOfWork
            .CourseRepository
            .GetWithFiltersAsync(
                category?.Id,
                request.AuthorId,
                CourseStatus.Published);
        
        return _mapper.Map<List<CourseDto>>(courses);
    }
}