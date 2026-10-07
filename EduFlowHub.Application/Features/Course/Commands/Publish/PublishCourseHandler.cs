using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.Course.Dtos;

namespace EduFlowHub.Application.Features.Course.Commands.Publish;

public class PublishCourseHandler
    : IRequestHandler<PublishCourseCommand, CourseDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public PublishCourseHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<CourseDto> Handle(
        PublishCourseCommand request, 
        CancellationToken cancellationToken)
    {
        var course = await _unitOfWork
            .CourseRepository
            .GetByIdAsync(request.CourseId);

        if (course is null)
        {
            throw new EntityNotFoundException("Course", request.CourseId);
        }
        
        course.Publish();

        await _unitOfWork
            .CourseRepository
            .UpdateAsync(course);

        await _unitOfWork.SaveAsync();
        return _mapper.Map<CourseDto>(course);
    }
}