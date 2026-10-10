using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Abstractions.Services;
using EduFlowHub.Application.Features.Authentication.Dtos;
using EduFlowHub.Application.Features.Authentication.Exceptions;
using EduFlowHub.Application.Features.User.Dtos;

namespace EduFlowHub.Application.Features.Authentication.Queries.Login;

public record LoginQuery(
    string Username,
    string Password)
    : IRequest<UserTokenDto>;

public class RegisterHandler
    : IRequestHandler<LoginQuery, UserTokenDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public RegisterHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<UserTokenDto> Handle(
        LoginQuery request,
        CancellationToken cancellationToken)
    {
        Domain.Entities.User? user = await _unitOfWork
            .UserRepository
            .GetByNameAsync(request.Username);

        if (user is null)
        {
            throw new UsernameNotFoundException(request.Username);
        }

        if (!_passwordHasher.VerifyHashedPassword(request.Password, user.PasswordHash))
        {
            throw new WrongPasswordException();

        }

        string jwtToken = _jwtTokenGenerator.GenerateJwtToken(user.Id, user.Role);

        var userDto = _mapper.Map<UserDto>(user);

        return new UserTokenDto(jwtToken, userDto);
    }
}