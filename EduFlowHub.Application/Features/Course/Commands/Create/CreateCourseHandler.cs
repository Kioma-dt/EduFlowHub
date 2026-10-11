using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.Course.Access.Service;
using EduFlowHub.Application.Features.Course.Dtos;
using EduFlowHub.Application.Features.CourseCategory.Dtos;
using EduFlowHub.Application.Features.User.Dtos;

namespace EduFlowHub.Application.Features.Course.Commands.Create;

public class CreateCourseHandler
    : IRequestHandler<CreateCourseCommand, CourseDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseAccessService _courseAccessService;

    public CreateCourseHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        ICourseAccessService courseAccessService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _courseAccessService = courseAccessService;
    }
    
    public async Task<CourseDto> Handle(
        CreateCourseCommand request, 
        CancellationToken cancellationToken)
    {
        _courseAccessService.EnsureCanCreate(request.AuthorId);
        
        Domain.Entities.User? author = await _unitOfWork
            .UserRepository
            .GetByIdAsync(request.AuthorId, cancellationToken);

        if (author is null)
        {
            throw new InvalidEntityReferenceException("User", request.AuthorId);
        }
        
        Domain.Entities.CourseCategory? category = await _unitOfWork
            .CourseCategoryRepository
            .GetByIdAsync(request.CategoryId, cancellationToken);

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
            .InsertAsync(course, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);
        
        var authorDto = _mapper.Map<UserDto>(author);
        var courseCategoryDto = _mapper.Map<CourseCategoryDto>(category);

        var courseDto = new CourseDto(
            course.Id,
            course.Title,
            course.Description,
            course.Price,
            course.Status,
            authorDto,
            courseCategoryDto);

        return courseDto;
    }
}