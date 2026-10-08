using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.User.Dtos;

namespace EduFlowHub.Application.Features.User.Commands.ChangeName;

public class ChangeUserNameHandler
    : IRequestHandler<ChangeUserNameCommand, UserDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeUserNameHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<UserDto> Handle(
        ChangeUserNameCommand request, 
        CancellationToken cancellationToken)
    {
        var user = await _unitOfWork
            .UserRepository
            .GetByIdAsync(request.UserId);

        if (user is null)
        {
            throw new EntityNotFoundException("User", 
                "Id",
                request.UserId.ToString());
        }
        
        var userDb = await _unitOfWork
            .UserRepository
            .GetByNameAsync(request.UserName);
        
        if (userDb is not null)
        {
            throw new EntityAlreadyExistsException("User", 
                "Name",
                request.UserName);
        }
        
        user.ChangeName(request.UserName);
        
        await _unitOfWork
            .UserRepository
            .UpdateAsync(user);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<UserDto>(user);
    }
}