using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.CourseEnrollment.Access.Service;
using EduFlowHub.Application.Features.CourseEnrollment.Exceptions;

namespace EduFlowHub.Application.Features.CourseEnrollment.Commands.UnenrollStudentFromCourse;

public class UnenrollStudentFromCourseHandler
    : IRequestHandler<UnenrollStudentFromCourseCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseEnrollmentAccessService _courseEnrollmentAccessService;

    public UnenrollStudentFromCourseHandler(
        IUnitOfWork unitOfWork,
        ICourseEnrollmentAccessService courseEnrollmentAccessService)
    {
        _unitOfWork = unitOfWork;
        _courseEnrollmentAccessService = courseEnrollmentAccessService;
    }
    
    public async Task Handle(
        UnenrollStudentFromCourseCommand request, 
        CancellationToken cancellationToken)
    {
        var courseEnrollment = await _unitOfWork
            .CourseEnrollmentRepository
            .GetByIdAsync(request.StudentId, request.CourseId, cancellationToken);
        
        if (courseEnrollment is null)
        {
            throw new StudentIsNotEnrolledOnCourseException(request.StudentId, request.CourseId);

        }
        
        _courseEnrollmentAccessService.EnsureCanUnEnroll(courseEnrollment);
        
        await _unitOfWork
            .CourseEnrollmentRepository
            .DeleteAsync(courseEnrollment, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);
    }
}