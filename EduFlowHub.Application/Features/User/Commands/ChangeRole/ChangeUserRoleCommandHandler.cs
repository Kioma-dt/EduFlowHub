using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.User.Dtos;

namespace EduFlowHub.Application.Features.User.Commands.ChangeRole;

public class ChangeUserRoleCommandHandler
    : IRequestHandler<ChangeUserRoleCommand, UserDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeUserRoleCommandHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<UserDto> Handle(
        ChangeUserRoleCommand request, 
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

        user.ChangeRole(request.UserRole);
        
        await _unitOfWork
            .UserRepository
            .UpdateAsync(user, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        return _mapper.Map<UserDto>(user);
    }
}