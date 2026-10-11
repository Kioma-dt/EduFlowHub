using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.CourseCategory.Access.Service;

namespace EduFlowHub.Application.Features.CourseCategory.Commands.Delete;

public class DeleteCourseCategoryHandler
    : IRequestHandler<DeleteCourseCategoryCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseCategoryAccessService _courseCategoryAccessService;

    public DeleteCourseCategoryHandler(
        IUnitOfWork unitOfWork,
        ICourseCategoryAccessService  courseCategoryAccessService)
    {
        _unitOfWork = unitOfWork;
        _courseCategoryAccessService = courseCategoryAccessService;
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
        
        _courseCategoryAccessService.EnsureCanDelete(courseCategory);
        
        if (await _unitOfWork.CourseRepository.ExistsAsync(course => course.CategoryId.Equals(courseCategory.Id), cancellationToken))
        {
            throw new CanNotDeleteEntityException("Can not delete Course Category due to dependent Courses");
        }
        
        await _unitOfWork
            .CourseCategoryRepository
            .DeleteAsync(courseCategory, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);
    }
}