using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.Course.Dtos;

namespace EduFlowHub.Application.Features.Course.Queries.ListAll;

public class ListAllCoursesHandler
    : IRequestHandler<ListAllCoursesQuery, IEnumerable<CourseDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ListAllCoursesHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<CourseDto>> Handle(
        ListAllCoursesQuery request, 
        CancellationToken cancellationToken)
    {
        Domain.Entities.CourseCategory? category = null;

        if (request.Category is not null)
        {
            category = await _unitOfWork
                .CourseCategoryRepository
                .GetByNormalizedNameAsync(request.Category, cancellationToken);
        }
        
        var courses = await _unitOfWork
            .CourseRepository
            .GetWithFiltersAsync(
                category?.Id,
                request.AuthorId,
                request.Status,
                cancellationToken);
        
        return _mapper.Map<List<CourseDto>>(courses);
    }
}