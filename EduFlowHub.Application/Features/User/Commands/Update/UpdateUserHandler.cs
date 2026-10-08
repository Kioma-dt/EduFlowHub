using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.User.Dtos;

namespace EduFlowHub.Application.Features.User.Commands.Update;

public class UpdateUserHandler
    : IRequestHandler<UpdateUserCommand, UserDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<UserDto> Handle(
        UpdateUserCommand request, 
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
        
        user.UpdateDetails(
            request.FirstName ?? user.FirstName,
            request.LastName ?? user.LastName,
            request.Email ?? user.Email,
            request.PhoneNumber ?? user.PhoneNumber);
        
        await _unitOfWork
            .UserRepository
            .UpdateAsync(user);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<UserDto>(user);
    }
}