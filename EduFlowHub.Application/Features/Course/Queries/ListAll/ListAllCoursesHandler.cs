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
        var courses = await _unitOfWork
            .CourseRepository
            .GetAllAsync(null,
                null,
                course => course.Author,
                course => course.Category);
        
        return _mapper.Map<List<CourseDto>>(courses);
    }
}