using EduFlowHub.Application.Abstractions.Persistence;

namespace EduFlowHub.Application.Features.CourseEnrollment.Queries.IsStudentEnrolledOnCourse;

public class IsStudentEnrolledOnCourseQueryHandler
    : IRequestHandler<IsStudentEnrolledOnCourseQuery, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public IsStudentEnrolledOnCourseQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<bool> Handle(
        IsStudentEnrolledOnCourseQuery request, 
        CancellationToken cancellationToken)
    {
        var courseEnrollment = await _unitOfWork
            .CourseEnrollmentRepository
            .GetByIdAsync(request.StudentId, request.CourseId, cancellationToken);
        
        return courseEnrollment is not null;
    }
}