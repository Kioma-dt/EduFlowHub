using EduFlowHub.Application.Abstractions.Persistence;

namespace EduFlowHub.Application.Features.User.Commands.Delete;

public class DeleteUserHandler
    : IRequestHandler<DeleteUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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
        
        
        await _unitOfWork
            .UserRepository
            .DeleteAsync(user, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);
    }
}