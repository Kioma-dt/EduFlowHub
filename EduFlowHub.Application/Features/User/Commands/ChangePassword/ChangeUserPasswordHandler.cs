using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Abstractions.Services.Authentication;
using EduFlowHub.Application.Exceptions.TooManyRequestsExceptions;
using EduFlowHub.Application.Features.User.Access.Service;
using EduFlowHub.Application.Features.User.Exceptions;
using EduFlowHub.Application.Features.User.Options;
using Microsoft.Extensions.Options;

namespace EduFlowHub.Application.Features.User.Commands.ChangePassword;

public class ChangeUserPasswordHandler
    : IRequestHandler<ChangeUserPasswordCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserAccessService  _userAccessService;
    private readonly UserLockoutOptions _userLockoutOptions;

    public ChangeUserPasswordHandler(
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        IUserAccessService userAccessService,
        IOptions<UserLockoutOptions>  userLockoutOptions)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher =  passwordHasher;
        _userAccessService = userAccessService;
        _userLockoutOptions = userLockoutOptions.Value;
    }
    
    public async Task Handle(
        ChangeUserPasswordCommand request, 
        CancellationToken cancellationToken)
    {
        DateTime now = DateTime.UtcNow;
        Domain.Entities.User?  user = await _unitOfWork
            .UserRepository
            .GetByIdAsync(request.UserId, cancellationToken);

        if (user is not null
            && user.IsLockedOut(now))
        {
            throw new UserIsBlockedExceptiom(user.LockoutEnd!.Value - now);
        }
        
        if (user is null)
        {
            throw new EntityNotFoundException("User", 
                "Id",
                request.UserId.ToString());
        }
        
        _userAccessService.EnsureCanChangePassword(user);

        if(!_passwordHasher.VerifyHashedPassword(request.OldPassword, user.PasswordHash))
        {
            user.FailedLoginAttempt(
                now, 
                _userLockoutOptions.MaxFailedAttempts,
                TimeSpan.FromMinutes(_userLockoutOptions.LockoutMinutes));
            await _unitOfWork.UserRepository.UpdateAsync(user, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
            throw new WrongPasswordException();
        }
        
        if (user.FailedLoginAttempts > 0 
            || user.LockoutEnd is not null)
        {
            user.ResetFailedLoginAttempts();
            await _unitOfWork.UserRepository.UpdateAsync(user, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
        }

        string passwordHash =  _passwordHasher.HashPassword(request.Password);
        
        user.ChangePasswordHash(passwordHash);
        
        await _unitOfWork
            .UserRepository
            .UpdateAsync(user, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);
    }
}