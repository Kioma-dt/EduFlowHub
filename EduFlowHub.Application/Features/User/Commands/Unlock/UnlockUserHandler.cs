using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.User.Access.Service;

namespace EduFlowHub.Application.Features.User.Commands.Unlock;

public class UnlockUserHandler
    : IRequestHandler<UnlockUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserAccessService  _userAccessService;

    public UnlockUserHandler(
        IUnitOfWork unitOfWork,
        IUserAccessService userAccessService)
    {
        _unitOfWork = unitOfWork;
        _userAccessService = userAccessService;
    }
    
    public async Task Handle(
        UnlockUserCommand request, 
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
        
        _userAccessService.EnsureCanUnlock(user);
        
        user.ResetFailedLoginAttempts();
        
        await _unitOfWork
            .UserRepository
            .UpdateAsync(user, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);
    }
}