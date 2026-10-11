using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.User.Access.Service;

namespace EduFlowHub.Application.Features.User.Commands.Delete;

public class DeleteUserHandler
    : IRequestHandler<DeleteUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserAccessService  _userAccessService;

    public DeleteUserHandler(
        IUnitOfWork unitOfWork,
        IUserAccessService userAccessService)
    {
        _unitOfWork = unitOfWork;
        _userAccessService = userAccessService;
    }
    
    public async Task Handle(
        DeleteUserCommand request, 
        CancellationToken cancellationToken)
    {
        var user = await _unitOfWork
            .UserRepository
            .GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            throw new EntityNotFoundException("User", 
                "Id",
                request.UserId.ToString());
        }
        
        _userAccessService.EnsureCanDelete(user);
        
        if (await _unitOfWork.CourseEnrollmentRepository.ExistsAsync(null, user.Id, cancellationToken))
        {
            throw new CanNotDeleteEntityException("Can not delete User due to dependent Enrollments");
        }
        
        if (await _unitOfWork.CourseRepository.ExistsAsync(course => course.AuthorId.Equals(user.Id), cancellationToken))
        {
            throw new CanNotDeleteEntityException("Can not delete User due to dependent Courses");
        }
        
        await _unitOfWork
            .UserRepository
            .DeleteAsync(user, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);
    }
}