using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.User.Dtos;

namespace EduFlowHub.Application.Features.User.Queries.GetByName;

public class GetUserByNameHandler
    : IRequestHandler<GetUserByNameQuery, UserDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetUserByNameHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }


    public async Task<UserDto> Handle(
        GetUserByNameQuery request,
        CancellationToken cancellationToken)
    {
        Domain.Entities.User? user = await _unitOfWork
            .UserRepository
            .GetByNameAsync(request.UserName, cancellationToken);

        if (user is null)
        {
            throw new EntityNotFoundException("User",
                "Name",
                request.UserName);
        }
        
        return _mapper.Map<UserDto>(user);
    }
}