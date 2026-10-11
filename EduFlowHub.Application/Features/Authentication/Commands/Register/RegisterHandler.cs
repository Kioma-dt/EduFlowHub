using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Abstractions.Services.Authentication;
using EduFlowHub.Application.Features.User.Dtos;

namespace EduFlowHub.Application.Features.Authentication.Commands.Register;

public class RegisterHandler
    : IRequestHandler<RegisterCommand, UserDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    
    private static readonly HashSet<UserRole> SelfRegisterRoles = new ()
    {
        UserRole.Student,
        UserRole.Teacher
    };

    public RegisterHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserDto> Handle(
        RegisterCommand request,
        CancellationToken cancellationToken)
    {
        if (!SelfRegisterRoles.Contains(request.Role))
        {
            throw new ApplicationValidationException($"You cannot register user with role {request.Role}");
        }

        if (await _unitOfWork.UserRepository.GetByNameAsync(request.Username, cancellationToken) is not null)
        {
            throw new EntityAlreadyExistsException(
                "User",
                "Name",
                request.Username);
        }

        string passwordHash = _passwordHasher.HashPassword(request.Password);

        var user = new Domain.Entities.User(
            request.Username,
            passwordHash,
            request.Role,
            request.FirstName,
            request.LastName);

        await _unitOfWork.UserRepository.InsertAsync(user, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        return _mapper.Map<UserDto>(user);
    }
}