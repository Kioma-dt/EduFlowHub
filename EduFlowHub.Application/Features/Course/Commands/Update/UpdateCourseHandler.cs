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
            throw new EntityNotFoundException("Course", request.CourseId);
        }
        
        course.Title = request.Title ?? course.Title;
        course.Description = request.Description ?? course.Description;
        course.Price = request.Price ?? course.Price;
        course.Status = request.Status ?? course.Status;

        if (request.AuthorId.HasValue)
        {
            var author = await _unitOfWork
                .UserRepository
                .GetByIdAsync(request.AuthorId.Value);

            if (author is null)
            {
                throw new InvalidEntityReferenceException("User", request.AuthorId.Value);
            }
            
            course.AuthorId = request.AuthorId.Value;
        }

        if (request.CategoryId.HasValue)
        {
            var category = await _unitOfWork
                .CourseCategoryRepository
                .GetByIdAsync(request.CategoryId.Value);

            if (category is null)
            {
                throw new InvalidEntityReferenceException("Category", request.CategoryId.Value);
            }
            
            course.CategoryId = request.CategoryId.Value;
        }
        

        await _unitOfWork
            .CourseRepository
            .UpdateAsync(course);

        await _unitOfWork.SaveAsync();
        return _mapper.Map<CourseDto>(course);
    }
}