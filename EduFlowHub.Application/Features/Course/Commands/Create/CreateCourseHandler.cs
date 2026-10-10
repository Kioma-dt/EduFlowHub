using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.Course.Dtos;

namespace EduFlowHub.Application.Features.Course.Commands.Create;

public class CreateCourseHandler
    : IRequestHandler<CreateCourseCommand, CourseDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCourseHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<CourseDto> Handle(
        CreateCourseCommand request, 
        CancellationToken cancellationToken)
    {
        Domain.Entities.User? author = await _unitOfWork
            .UserRepository
            .GetByIdAsync(request.AuthorId);

        if (author is null)
        {
            throw new InvalidEntityReferenceException("User", request.AuthorId);
        }
        
        Domain.Entities.CourseCategory? category = await _unitOfWork
            .CourseCategoryRepository
            .GetByIdAsync(request.CategoryId);

        if (category is null)
        {
            throw new InvalidEntityReferenceException("Category", request.CategoryId);
        }

        var course = new Domain.Entities.Course(
            request.Title,
            request.Description,
            request.Price,
            request.AuthorId,
            request.CategoryId);
        
        await _unitOfWork
            .CourseRepository
            .InsertAsync(course);
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