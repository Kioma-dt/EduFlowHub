using EduFlowHub.Application.Abstractions.Persistence;

namespace EduFlowHub.Application.Features.CourseEnrollment.Commands.EnrollStudentOnCourse;

public class EnrollStudentOnCourseHandler
    : IRequestHandler<EnrollStudentOnCourseCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public EnrollStudentOnCourseHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(
        EnrollStudentOnCourseCommand request, 
        CancellationToken cancellationToken)
    {
        var student = await _unitOfWork
            .UserRepository
            .GetByIdAsync(request.StudentId);

        if (student is null)
        {
            throw new InvalidEntityReferenceException("Student", request.StudentId);
        }
        
        var course = await _unitOfWork
            .CourseRepository
            .GetByIdAsync(request.CourseId);

        if (course is null)
        {
            throw new InvalidEntityReferenceException("Course", request.CourseId);
        }

        var enrollment = new Domain.Entities.CourseEnrollment(
            request.CourseId, 
            request.StudentId);
        
        await _unitOfWork
            .CourseEnrollmentRepository
            .InsertAsync(enrollment);
        await _unitOfWork.SaveAsync();
    }
}