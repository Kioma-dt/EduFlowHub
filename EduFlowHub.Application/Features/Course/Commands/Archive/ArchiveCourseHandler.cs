using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Exceptions;
using EduFlowHub.Application.Features.Course.Access.Service;
using EduFlowHub.Application.Features.Course.Dtos;

namespace EduFlowHub.Application.Features.Course.Commands.Archive;

public class ArchiveCourseHandler
    : IRequestHandler<ArchiveCourseCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseAccessService _courseAccessService;

    public ArchiveCourseHandler(
        IUnitOfWork unitOfWork,
        ICourseAccessService courseAccessService)
    {
        _unitOfWork = unitOfWork;
        _courseAccessService = courseAccessService;
    }
    
    public async Task Handle(
        ArchiveCourseCommand request, 
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
        
        _courseAccessService.EnsureCanArchive(course);
        
        course.Archive();

        await _unitOfWork
            .CourseRepository
            .UpdateAsync(course, cancellationToken);

        await _unitOfWork.SaveAsync(cancellationToken);
    }
}