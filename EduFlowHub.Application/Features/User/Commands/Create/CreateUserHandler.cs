using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Abstractions.Services;
using EduFlowHub.Application.Abstractions.Services.Authentication;
using EduFlowHub.Application.Features.User.Dtos;

namespace EduFlowHub.Application.Features.User.Commands.Create;

public class CreateUserHandler
    : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public CreateUserHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _passwordHasher =  passwordHasher;
    }
    
    public async Task<UserDto> Handle(
        CreateUserCommand request, 
        CancellationToken cancellationToken)
    {
        var userDb = await _unitOfWork
            .UserRepository
            .GetByNameAsync(request.Username, cancellationToken);

        if (userDb is not null)
        {
            throw new EntityAlreadyExistsException("User", 
                "Name",
                request.Username);
        }
        
        string passwordHash =  _passwordHasher.HashPassword(request.Password);
        
        var user = new Domain.Entities.User(
            request.Username,
            passwordHash,
            request.Role,
            request.FirstName,
            request.LastName);
        
        await _unitOfWork
            .UserRepository
            .InsertAsync(user, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);
        

        return _mapper.Map<UserDto>(user);
    }
}