using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.Course.Access.Service;

namespace EduFlowHub.Application.Features.Course.Commands.Publish;

public class PublishCourseHandler
    : IRequestHandler<PublishCourseCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseAccessService _courseAccessService;

    public PublishCourseHandler(
        IUnitOfWork unitOfWork,
        ICourseAccessService courseAccessService)
    {
        _unitOfWork = unitOfWork;
        _courseAccessService = courseAccessService;
    }
    
    public async Task Handle(
        PublishCourseCommand request, 
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
        
        _courseAccessService.EnsureCanPublish(course);
        
        course.Publish();

        await _unitOfWork
            .CourseRepository
            .UpdateAsync(course, cancellationToken);

        await _unitOfWork.SaveAsync(cancellationToken);
    }
}