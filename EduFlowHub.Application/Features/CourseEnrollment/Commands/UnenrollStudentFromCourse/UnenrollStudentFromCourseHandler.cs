using EduFlowHub.Application.Abstractions.Persistence;

namespace EduFlowHub.Application.Features.CourseEnrollment.Commands.UnenrollStudentFromCourse;

public class UnenrollStudentFromCourseHandler
    : IRequestHandler<UnenrollStudentFromCourseCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UnenrollStudentFromCourseHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(
        UnenrollStudentFromCourseCommand request, 
        CancellationToken cancellationToken)
    {
        if (!await _unitOfWork
                .CourseEnrollmentRepository
                .ExistsAsync(
                    request.StudentId,
                    request.CourseId))
        {
            throw new EntityNotFoundException(
                "CourseEnrollment",
                "Student and Course Id",
                $"{request.StudentId.ToString()} and {request.CourseId.ToString()}");

        }
        await _unitOfWork
            .CourseEnrollmentRepository
            .DeleteAsync(request.StudentId, request.CourseId);
        await _unitOfWork.SaveAsync();
    }
}