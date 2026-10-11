using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.User.Dtos;

namespace EduFlowHub.Application.Features.CourseEnrollment.Queries.GetEnrolledStudentsOnCourse;

public class GetEnrolledStudentsOnCourseHandler
    : IRequestHandler<GetEnrolledStudentsOnCourseQuery, IEnumerable<UserDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetEnrolledStudentsOnCourseHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<UserDto>> Handle(
        GetEnrolledStudentsOnCourseQuery request, 
        CancellationToken cancellationToken)
    {
        IEnumerable<Domain.Entities.User> users = await _unitOfWork
            .UserRepository
            .GetEnrolledOnCourse(request.CourseId, cancellationToken);
        
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }
}