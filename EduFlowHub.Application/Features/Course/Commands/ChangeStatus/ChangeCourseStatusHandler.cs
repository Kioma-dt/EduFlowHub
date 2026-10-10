using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.Course.Dtos;

namespace EduFlowHub.Application.Features.Course.Commands.ChangeStatus;

public class ChangeCourseStatusHandler
    : IRequestHandler<ChangeCourseStatusCommand, CourseDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeCourseStatusHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<CourseDto> Handle(
        ChangeCourseStatusCommand request,
        CancellationToken cancellationToken)
    {
        Domain.Entities.Course? course = await _unitOfWork
            .CourseRepository
            .GetByIdAsync(request.CourseId,
                course => course.Author!,
                course => course.Category!);

        if (course is null)
        {
            throw new EntityNotFoundException(
                "Course",
                "Id",
                request.CourseId.ToString());
        }

        course.ChangeStatus(request.Status);
        
        await _unitOfWork
            .CourseRepository
            .UpdateAsync(course);

        await _unitOfWork.SaveAsync();
        
        return _mapper.Map<CourseDto>(course);
    }
}