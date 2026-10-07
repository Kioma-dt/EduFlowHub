using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.Course.Dtos;

namespace EduFlowHub.Application.Features.Course.Queries.GetById;

public class GetCourseByIdHandler
    : IRequestHandler<GetCourseByIdQuery, CourseDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetCourseByIdHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<CourseDto> Handle(
        GetCourseByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var course = await _unitOfWork
            .CourseRepository
            .GetByIdAsync(request.CourseId);

        if (course is null)
        {
            throw new EntityNotFoundException("Course", request.CourseId);
        }
        
        return _mapper.Map<CourseDto>(course);
    }
}