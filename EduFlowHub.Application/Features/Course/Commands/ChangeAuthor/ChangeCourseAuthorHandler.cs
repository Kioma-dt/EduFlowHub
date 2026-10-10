using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.Course.Access.Service;
using EduFlowHub.Application.Features.Course.Dtos;

namespace EduFlowHub.Application.Features.Course.Commands.ChangeAuthor;

public class ChangeCourseAuthorHandler
    : IRequestHandler<ChangeCourseAuthorCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseAccessService _courseAccessService;

    private static readonly HashSet<UserRole> AllowedAuthors = new ()
    {
        UserRole.Teacher
    };

    public ChangeCourseAuthorHandler(
        IUnitOfWork unitOfWork,
        ICourseAccessService courseAccessService)
    {
        _unitOfWork = unitOfWork;
        _courseAccessService = courseAccessService;
    }

    public async Task Handle(
        ChangeCourseAuthorCommand request, 
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
        
        _courseAccessService.EnsureCanChangeAuthor(course);

        Domain.Entities.User? newAuthor = await _unitOfWork
            .UserRepository
            .GetByIdAsync(request.AuthorId, cancellationToken);
        
        if (newAuthor is null)
        {
            throw new InvalidEntityReferenceException("User", request.AuthorId);
        }

        if (!AllowedAuthors.Contains(newAuthor.Role))
        {
            throw new ApplicationException($"Author {newAuthor.Role} is not allowed");
        }

        if (newAuthor.Id == course.AuthorId)
        {
            return;
        }
            
        course.ChangeAuthor(request.AuthorId);

        await _unitOfWork
            .CourseRepository
            .UpdateAsync(course, cancellationToken);

        await _unitOfWork.SaveAsync(cancellationToken);
    }
}