using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.Course.Access.Service;

namespace EduFlowHub.Application.Features.Course.Commands.Restore;

public class RestoreCourseHandler
    : IRequestHandler<RestoreCourseCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseAccessService _courseAccessService;

    public RestoreCourseHandler(
        IUnitOfWork unitOfWork,
        ICourseAccessService courseAccessService)
    {
        _unitOfWork = unitOfWork;
        _courseAccessService = courseAccessService;
    }

    public async Task Handle(
        RestoreCourseCommand request,
        CancellationToken cancellationToken)
    {
        Domain.Entities.Course? course = await _unitOfWork
            .CourseRepository
            .GetByIdAsync(
                request.CourseId,
                cancellationToken,
                course => course.LessonMaterials);

        if (course is null)
        {
            throw new EntityNotFoundException(
                "Course",
                "Id",
                request.CourseId.ToString());
        }
        
        _courseAccessService.EnsureCanRestore(course);
        
        course.Restore();
        
        await _unitOfWork
            .CourseRepository
            .UpdateAsync(course, cancellationToken);

        await _unitOfWork.SaveAsync(cancellationToken);
    }
}