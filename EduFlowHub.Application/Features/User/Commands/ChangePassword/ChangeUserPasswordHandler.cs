using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Abstractions.Services.Authentication;
using EduFlowHub.Application.Features.User.Access.Service;
using EduFlowHub.Application.Features.User.Exceptions;

namespace EduFlowHub.Application.Features.User.Commands.ChangePassword;

public class ChangeUserPasswordHandler
    : IRequestHandler<ChangeUserPasswordCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserAccessService  _userAccessService;

    public ChangeUserPasswordHandler(
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        IUserAccessService userAccessService)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher =  passwordHasher;
        _userAccessService = userAccessService;
    }
    
    public async Task Handle(
        ChangeUserPasswordCommand request, 
        CancellationToken cancellationToken)
    {
        Domain.Entities.User?  user = await _unitOfWork
            .UserRepository
            .GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            throw new EntityNotFoundException("User", 
                "Id",
                request.UserId.ToString());
        }
        
        _userAccessService.EnsureCanChangePassword(user);

        if(!_passwordHasher.VerifyHashedPassword(request.OldPassword, user.PasswordHash))
        {
            throw new WrongPasswordException();
        }

        string passwordHash =  _passwordHasher.HashPassword(request.Password);
        
        user.ChangePasswordHash(passwordHash);
        
        await _unitOfWork
            .UserRepository
            .UpdateAsync(user, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);
    }
}