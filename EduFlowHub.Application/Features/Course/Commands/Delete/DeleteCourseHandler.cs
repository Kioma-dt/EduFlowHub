using EduFlowHub.Application.Abstractions.Persistence;

namespace EduFlowHub.Application.Features.Course.Commands.Delete;

public class DeleteCourseHandler
    : IRequestHandler<DeleteCourseCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCourseHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(
        DeleteCourseCommand request, 
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
        
        await _unitOfWork
            .CourseRepository
            .DeleteAsync(course);

        await _unitOfWork.SaveAsync();
    }
}