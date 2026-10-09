using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.User.Dtos;

namespace EduFlowHub.Application.Features.User.Queries.List;

public class ListAllUsersHandler
    : IRequestHandler<ListAllUsersQuery, IEnumerable<UserDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ListAllUsersHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }


    public async Task<IEnumerable<UserDto>> Handle(
        ListAllUsersQuery request,
        CancellationToken cancellationToken)
    {
        IEnumerable<Domain.Entities.User> users = await _unitOfWork
            .UserRepository
            .GetAllAsync();

        return _mapper.Map<IEnumerable<UserDto>>(users);
    }
}