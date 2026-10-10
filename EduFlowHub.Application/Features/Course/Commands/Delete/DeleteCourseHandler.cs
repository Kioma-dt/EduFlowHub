using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.Course.Access.Service;

namespace EduFlowHub.Application.Features.Course.Commands.Delete;

public class DeleteCourseHandler
    : IRequestHandler<DeleteCourseCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseAccessService _courseAccessService;

    public DeleteCourseHandler(
        IUnitOfWork unitOfWork,
        ICourseAccessService courseAccessService)
    {
        _unitOfWork = unitOfWork;
        _courseAccessService = courseAccessService;
    }
    
    public async Task Handle(
        DeleteCourseCommand request, 
        CancellationToken cancellationToken)
    {
        Domain.Entities.Course? course = await _unitOfWork
            .CourseRepository
            .GetByIdAsync(request.CourseId, cancellationToken);

        if (course is null)
        {
            throw new EntityNotFoundException(
                "Course",
                "Id",
                request.CourseId.ToString());
        }
        
        _courseAccessService.EnsureCanDelete(course);

        IEnumerable<Domain.Entities.CourseEnrollment> enrollments = await _unitOfWork
            .CourseEnrollmentRepository
            .GetByCourseIdAsync(course.Id, cancellationToken);

        if (enrollments.Any())
        {
            throw new CanNotDeleteEntityException("Can not delete Course due to multiple enrollments");
        }
        
        await _unitOfWork
            .CourseRepository
            .DeleteAsync(course, cancellationToken);

        await _unitOfWork.SaveAsync(cancellationToken);
    }
}