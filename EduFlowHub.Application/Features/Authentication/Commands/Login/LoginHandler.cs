using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Abstractions.Services.Authentication;
using EduFlowHub.Application.Exceptions.TooManyRequestsExceptions;
using EduFlowHub.Application.Features.Authentication.Dtos;
using EduFlowHub.Application.Features.Authentication.Exceptions;
using EduFlowHub.Application.Features.User.Dtos;
using EduFlowHub.Application.Features.User.Options;
using Microsoft.Extensions.Options;

namespace EduFlowHub.Application.Features.Authentication.Commands.Login;

public class LoginHandler
    : IRequestHandler<LoginCommand, UserTokenDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly UserLockoutOptions  _userLockoutOptions;

    public LoginHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator,
        IOptions<UserLockoutOptions> userLockoutOptions)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _userLockoutOptions = userLockoutOptions.Value;
    }

    public async Task<UserTokenDto> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        DateTime now = DateTime.UtcNow;
        
        Domain.Entities.User? user = await _unitOfWork
            .UserRepository
            .GetByNameAsync(request.Username, cancellationToken);

        if (user is not null
            && user.IsLockedOut(now))
        {
            throw new UserIsBlockedExceptiom(user.LockoutEnd!.Value - now);
        }

        if (user is null
            || !_passwordHasher.VerifyHashedPassword(request.Password, user.PasswordHash)) 
        {
            if (user is not null)
            {
                user.FailedLoginAttempt(
                    now, 
                    _userLockoutOptions.MaxFailedAttempts,
                    TimeSpan.FromMinutes(_userLockoutOptions.LockoutMinutes));
                await _unitOfWork.UserRepository.UpdateAsync(user, cancellationToken);
                await _unitOfWork.SaveAsync(cancellationToken);
            }
            
            throw new LoginFailedException();
        }
        
        if (user.FailedLoginAttempts > 0 
            || user.LockoutEnd is not null)
        {
            user.ResetFailedLoginAttempts();
            await _unitOfWork.UserRepository.UpdateAsync(user, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
        }

        string jwtToken = _jwtTokenGenerator.GenerateJwtToken(user.Id, user.Role);

        // var userDto = _mapper.Map<UserDto>(user);
        var userDto = new UserDto(user.Id, user.Name, user.Role, user.FirstName, user.LastName);

        return new UserTokenDto(jwtToken, userDto);
    }
}