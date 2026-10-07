using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Exceptions;
using EduFlowHub.Application.Features.Course.Dtos;

namespace EduFlowHub.Application.Features.Course.Commands.Archive;

public class ArchiveCourseHandler
    : IRequestHandler<ArchiveCourseCommand, CourseDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ArchiveCourseHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<CourseDto> Handle(
        ArchiveCourseCommand request, 
        CancellationToken cancellationToken)
    {
        var course = await _unitOfWork
            .CourseRepository
            .GetByIdAsync(
                request.CourseId,
                course => course.Author,
                course => course.Category);

        if (course is null)
        {
            throw new EntityNotFoundException("Course", request.CourseId);
        }
        
        course.Archive();

        await _unitOfWork
            .CourseRepository
            .UpdateAsync(course);

        await _unitOfWork.SaveAsync();
        return _mapper.Map<CourseDto>(course);
    }
}