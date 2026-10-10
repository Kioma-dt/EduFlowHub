using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.Course.Dtos;

namespace EduFlowHub.Application.Features.Course.Commands.ChangeAuthor;

public class ChangeCourseAuthorHandler
    : IRequestHandler<ChangeCourseAuthorCommand, CourseDto>
{
    IMapper _mapper;
    IUnitOfWork _unitOfWork;

    public ChangeCourseAuthorHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<CourseDto> Handle(
        ChangeCourseAuthorCommand request, 
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
        
        Domain.Entities.User? author = await _unitOfWork
            .UserRepository
            .GetByIdAsync(request.AuthorId);

        if (author is null)
        {
            throw new InvalidEntityReferenceException("User", request.AuthorId);
        }
            
        course.ChangeAuthor(request.AuthorId);

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