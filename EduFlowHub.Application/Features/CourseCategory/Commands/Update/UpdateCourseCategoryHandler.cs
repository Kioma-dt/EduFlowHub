using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.CourseCategory.Dtos;

namespace EduFlowHub.Application.Features.CourseCategory.Commands.Update;

public class UpdateCourseCategoryHandler
    : IRequestHandler<UpdateCourseCategoryCommand, CourseCategoryDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCourseCategoryHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<CourseCategoryDto> Handle(
        UpdateCourseCategoryCommand request, 
        CancellationToken cancellationToken)
    {
        var courseCategory = await _unitOfWork
            .CourseCategoryRepository
            .GetByIdAsync(request.CourseCategoryId);

        if (courseCategory is null)
        {
            throw new EntityNotFoundException(
                "Course Category", 
                "Id",
                request.CourseCategoryId.ToString());
        }
        
        courseCategory.UpdateDetails(
            request.Name ?? courseCategory.Name);
        
        await _unitOfWork
            .CourseCategoryRepository
            .UpdateAsync(courseCategory);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<CourseCategoryDto>(courseCategory);
    }
}