using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.Course.Access.Service;
using EduFlowHub.Application.Features.CourseEnrollment.Access.Policy;
using EduFlowHub.Application.Features.CourseEnrollment.Access.Service;
using EduFlowHub.Application.Features.CourseEnrollment.Exceptions;
using EduFlowHub.Domain.Exceptions;

namespace EduFlowHub.Application.Features.CourseEnrollment.Commands.EnrollStudentOnCourse;

public class EnrollStudentOnCourseHandler
    : IRequestHandler<EnrollStudentOnCourseCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseEnrollmentAccessService _courseAccessService;

    public EnrollStudentOnCourseHandler(
        IUnitOfWork unitOfWork,
        ICourseEnrollmentAccessService courseAccessService)
    {
        _unitOfWork = unitOfWork;
        _courseAccessService = courseAccessService;
    }
    
    public async Task Handle(
        EnrollStudentOnCourseCommand request, 
        CancellationToken cancellationToken)
    {
        _courseAccessService.EnsureCanEnroll(request.StudentId);
        
        if (await _unitOfWork
                .CourseEnrollmentRepository
                .GetByIdAsync(request.StudentId, request.CourseId, cancellationToken) is not null)
        {
            throw new StudentAlreadyEnrolledOnCourseException(request.StudentId, request.CourseId);
        }
        
        var student = await _unitOfWork
            .UserRepository
            .GetByIdAsync(request.StudentId, cancellationToken);

        if (student is null)
        {
            throw new InvalidEntityReferenceException("Student", request.StudentId);
        }
        
        var course = await _unitOfWork
            .CourseRepository
            .GetByIdAsync(request.CourseId, cancellationToken);

        if (course is null)
        {
            throw new InvalidEntityReferenceException("Course", request.CourseId);
        }

        course.EnsureCanEnrollStudent();
        
        var enrollment = new Domain.Entities.CourseEnrollment(
            request.CourseId, 
            request.StudentId);
        
        await _unitOfWork
            .CourseEnrollmentRepository
            .InsertAsync(enrollment, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);
    }
}
