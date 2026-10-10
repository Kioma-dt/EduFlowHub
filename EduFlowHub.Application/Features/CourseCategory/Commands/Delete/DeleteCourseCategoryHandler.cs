using EduFlowHub.Application.Abstractions.Persistence;

namespace EduFlowHub.Application.Features.CourseCategory.Commands.Delete;

public class DeleteCourseCategoryHandler
    : IRequestHandler<DeleteCourseCategoryCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCourseCategoryHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(
        DeleteCourseCategoryCommand request, 
        CancellationToken cancellationToken)
    {
        var courseCategory = await _unitOfWork
            .CourseCategoryRepository
            .GetByIdAsync(request.CourseCategoryId, cancellationToken);

        if (courseCategory is null)
        {
            throw new EntityNotFoundException(
                "Course Category", 
                "Id",
                request.CourseCategoryId.ToString());
        }
        
        await _unitOfWork
            .CourseCategoryRepository
            .DeleteAsync(courseCategory, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);
    }
}