using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Abstractions.Services;
using EduFlowHub.Application.Abstractions.Services.Authentication;
using EduFlowHub.Application.Features.User.Dtos;
using EduFlowHub.Application.Features.User.Exceptions;

namespace EduFlowHub.Application.Features.User.Commands.ChangePassword;

public class ChangeUserPasswordHandler
    : IRequestHandler<ChangeUserPasswordCommand, UserDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public ChangeUserPasswordHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _passwordHasher =  passwordHasher;
    }
    
    public async Task<UserDto> Handle(
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

        if (request.OldPassword is null
            || !_passwordHasher.VerifyHashedPassword(
                request.OldPassword,
                user.PasswordHash))
        {
            throw new WrongPasswordException();
        }
        
        string passwordHash =  _passwordHasher.HashPassword(request.Password);
        
        user.ChangePasswordHash(passwordHash);
        
        await _unitOfWork
            .UserRepository
            .UpdateAsync(user, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);
        
        return _mapper.Map<UserDto>(user);
    }
}