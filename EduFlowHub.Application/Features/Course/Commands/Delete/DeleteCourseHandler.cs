using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.Course.Access.Service;
using EduFlowHub.Domain.Exceptions;

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

        if (course.Status != CourseStatus.Draft)
        {
            throw new DomainWrongStatusException("Can not change not draft course");
        }
        
        if (await _unitOfWork.CourseEnrollmentRepository.ExistsAsync(course.Id, null, cancellationToken))
        {
            throw new CanNotDeleteEntityException("Can not delete Course due to dependent Enrollments");
        }
        
        await _unitOfWork
            .CourseRepository
            .DeleteAsync(course, cancellationToken);

        await _unitOfWork.SaveAsync(cancellationToken);
    }
}