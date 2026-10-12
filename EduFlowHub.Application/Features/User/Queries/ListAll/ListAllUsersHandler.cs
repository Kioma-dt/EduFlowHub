using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.Dtos;
using EduFlowHub.Application.Features.User.Dtos;

namespace EduFlowHub.Application.Features.User.Queries.ListAll;

public class ListAllUsersHandler
    : IRequestHandler<ListAllUsersQuery, PagedDto<UserDto>>
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


    public async Task<PagedDto<UserDto>> Handle(
        ListAllUsersQuery request,
        CancellationToken cancellationToken)
    {
        IEnumerable<Domain.Entities.User> users = await _unitOfWork
            .UserRepository
            .GetAllAsync(null, null, request.Page, request.PageSize,cancellationToken);

        var items = _mapper.Map<IEnumerable<UserDto>>(users);
        int totalCount = await _unitOfWork
            .UserRepository
            .CountAsync(null, cancellationToken);
        
        return new PagedDto<UserDto>(items, request.Page, request.PageSize, totalCount);
    }
}