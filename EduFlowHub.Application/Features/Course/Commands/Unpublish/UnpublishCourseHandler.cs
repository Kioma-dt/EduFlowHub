using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.Course.Access.Service;

namespace EduFlowHub.Application.Features.Course.Commands.Unpublish;

public class UnpublishCourseHandler
    : IRequestHandler<UnpublishCourseCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseAccessService _courseAccessService;

    public UnpublishCourseHandler(
        IUnitOfWork unitOfWork,
        ICourseAccessService courseAccessService)
    {
        _unitOfWork = unitOfWork;
        _courseAccessService = courseAccessService;
    }

    public async Task Handle(
        UnpublishCourseCommand request,
        CancellationToken cancellationToken)
    {
        Domain.Entities.Course? course = await _unitOfWork
            .CourseRepository
            .GetByIdAsync(request.CourseId);

        if (course is null)
        {
            throw new EntityNotFoundException(
                "Course",
                "Id",
                request.CourseId.ToString());
        }
        
        _courseAccessService.EnsureCanUnpublish(course);
        
        course.Unpublish();
        
        await _unitOfWork
            .CourseRepository
            .UpdateAsync(course);

        await _unitOfWork.SaveAsync();
    }
}