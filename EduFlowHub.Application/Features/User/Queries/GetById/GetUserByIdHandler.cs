using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.User.Dtos;

namespace EduFlowHub.Application.Features.User.Queries.GetById;

public class GetUserByIdHandler
    : IRequestHandler<GetUserByIdQuery, UserDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetUserByIdHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<UserDto> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        Domain.Entities.User? user = await _unitOfWork
            .UserRepository
            .GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            throw new EntityNotFoundException(
                "User",
                "Id",
                request.UserId.ToString());
        }
        
        return _mapper.Map<UserDto>(user);
    }
}