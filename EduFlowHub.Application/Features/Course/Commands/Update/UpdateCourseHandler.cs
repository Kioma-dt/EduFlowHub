using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.Course.Dtos;

namespace EduFlowHub.Application.Features.Course.Commands.Update;

public class UpdateCourseHandler
    : IRequestHandler<UpdateCourseCommand, CourseDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCourseHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<CourseDto> Handle(
        UpdateCourseCommand request, 
        CancellationToken cancellationToken)
    {
        var course = await _unitOfWork
            .CourseRepository
            .GetByIdAsync(request.CourseId);

        if (course is null)
        {
            throw new EntityNotFoundException(
                "Course",
                "Id",
                request.CourseId.ToString());
        }
        
        course.UpdateDetails(
            request.Title ?? course.Title,
            request.Description  ?? course.Description,
            request.Price ??  course.Price);

        if (request.CategoryId.HasValue)
        {
            var category = await _unitOfWork
                .CourseCategoryRepository
                .GetByIdAsync(request.CategoryId.Value);

            if (category is null)
            {
                throw new InvalidEntityReferenceException("Category", request.CategoryId.Value);
            }
            
            course.ChangeCategory(request.CategoryId.Value);
        }
        

        await _unitOfWork
            .CourseRepository
            .UpdateAsync(course);

        await _unitOfWork.SaveAsync();
        
        Domain.Entities.Course courseWithNavigations = await _unitOfWork
            .CourseRepository
            .GetByIdAsync(
                course.Id,
                course => course.Author,
                course => course.Category);
        
        return _mapper.Map<CourseDto>(courseWithNavigations);
    }
}