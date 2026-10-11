using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.Course.Access.Service;
using EduFlowHub.Application.Features.Course.Dtos;
using EduFlowHub.Domain.Exceptions;

namespace EduFlowHub.Application.Features.Course.Commands.Update;

public class UpdateCourseHandler
    : IRequestHandler<UpdateCourseCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseAccessService _courseAccessService;

    public UpdateCourseHandler(
        IUnitOfWork unitOfWork,
        ICourseAccessService courseAccessService)
    {
        _unitOfWork = unitOfWork;
        _courseAccessService = courseAccessService;
    }
    
    public async Task Handle(
        UpdateCourseCommand request, 
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
        
        _courseAccessService.EnsureCanUpdate(course);
        
        if (course.Status != CourseStatus.Draft)
        {
            throw new DomainWrongStatusException("Can not change not draft course");
        }
        
        course.UpdateDetails(
            request.Title ?? course.Title,
            request.Description  ?? course.Description,
            request.Price ??  course.Price);

        if (request.CategoryId.HasValue)
        {
            Domain.Entities.CourseCategory? category = await _unitOfWork
                .CourseCategoryRepository
                .GetByIdAsync(request.CategoryId.Value, cancellationToken);

            if (category is null)
            {
                throw new InvalidEntityReferenceException("Category", request.CategoryId.Value);
            }
            
            course.ChangeCategory(request.CategoryId.Value);
        }

        await _unitOfWork
            .CourseRepository
            .UpdateAsync(course, cancellationToken);

        await _unitOfWork.SaveAsync(cancellationToken);
    }
}