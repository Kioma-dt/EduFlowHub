using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.Course.Dtos;

namespace EduFlowHub.Application.Features.Course.Queries.ListEnrollerd;

public class ListEnrollerdCoursesHandler
    : IRequestHandler<ListEnrolledCoursesQuery, IEnumerable<CourseDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ListEnrollerdCoursesHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<CourseDto>> Handle(
        ListEnrolledCoursesQuery request, 
        CancellationToken cancellationToken)
    {
        var courses = await _unitOfWork
            .CourseRepository
            .GetEnrolledCoursesAsync(request.StudentId, cancellationToken);
        
        return _mapper.Map<List<CourseDto>>(courses);
    }
}