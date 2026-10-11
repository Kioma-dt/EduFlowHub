using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.User.Access.Service;

namespace EduFlowHub.Application.Features.User.Commands.Update;

public class UpdateUserHandler
    : IRequestHandler<UpdateUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserAccessService  _userAccessService;

    public UpdateUserHandler(
        IUnitOfWork unitOfWork,
        IUserAccessService  userAccessService)
    {
        _unitOfWork = unitOfWork;
        _userAccessService = userAccessService;
    }
    
    public async Task Handle(
        UpdateUserCommand request, 
        CancellationToken cancellationToken)
    {
        var user = await _unitOfWork
            .UserRepository
            .GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            throw new EntityNotFoundException(
                "User", 
                "Id",
                request.UserId.ToString());
        }
        
        _userAccessService.EnsureCanUpdate(user);
        
        user.UpdateDetails(
            request.FirstName ?? user.FirstName,
            request.LastName ?? user.LastName);
        
        await _unitOfWork
            .UserRepository
            .UpdateAsync(user, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);
    }
}