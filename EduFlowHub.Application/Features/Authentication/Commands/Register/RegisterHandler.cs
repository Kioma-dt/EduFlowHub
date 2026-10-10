using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Abstractions.Services;
using EduFlowHub.Application.Features.User.Dtos;

namespace EduFlowHub.Application.Features.Authentication.Commands.Register;

public class RegisterHandler
    : IRequestHandler<RegisterCommand, UserDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

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
        if (request.Role == UserRole.Admin)
        {
            throw new ApplicationException("You cannot register admin users");
        }
        
        Domain.Entities.User? userDb = await _unitOfWork
            .UserRepository
            .GetByNameAsync(request.Username);

        if (userDb is not null)
        {
            throw new EntityAlreadyExistsException("User",
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

        await _unitOfWork
            .UserRepository
            .InsertAsync(user);
        await _unitOfWork.SaveAsync();


        return _mapper.Map<UserDto>(user);
    }
}