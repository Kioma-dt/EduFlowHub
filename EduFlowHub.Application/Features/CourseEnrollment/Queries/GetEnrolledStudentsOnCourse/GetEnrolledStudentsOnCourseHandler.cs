using AutoMapper.Internal.Mappers;
using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.Dtos;
using EduFlowHub.Application.Features.User.Dtos;

namespace EduFlowHub.Application.Features.CourseEnrollment.Queries.GetEnrolledStudentsOnCourse;

public class GetEnrolledStudentsOnCourseHandler
    : IRequestHandler<GetEnrolledStudentsOnCourseQuery, PagedDto<UserDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetEnrolledStudentsOnCourseHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<PagedDto<UserDto>> Handle(
        GetEnrolledStudentsOnCourseQuery request, 
        CancellationToken cancellationToken)
    {
        IEnumerable<Domain.Entities.User> users = await _unitOfWork
            .UserRepository
            .GetEnrolledOnCourse(
                request.CourseId, 
                null,
                null,
                request.Page,
                request.PageSize,
                cancellationToken);

        var items = _mapper.Map<IEnumerable<UserDto>>(users);
        int totalCount = await _unitOfWork
            .UserRepository
            .CountAsync(null, cancellationToken);
        
        return new PagedDto<UserDto>(items, request.Page, request.PageSize, totalCount);
    }
}