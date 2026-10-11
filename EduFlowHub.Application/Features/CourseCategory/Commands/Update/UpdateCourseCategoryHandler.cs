using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.CourseCategory.Access.Service;
using EduFlowHub.Application.Features.CourseCategory.Dtos;

namespace EduFlowHub.Application.Features.CourseCategory.Commands.Update;

public class UpdateCourseCategoryHandler
    : IRequestHandler<UpdateCourseCategoryCommand, CourseCategoryDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseCategoryAccessService _courseCategoryAccessService;

    public UpdateCourseCategoryHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        ICourseCategoryAccessService courseCategoryAccessService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _courseCategoryAccessService = courseCategoryAccessService;
    }
    
    public async Task<CourseCategoryDto> Handle(
        UpdateCourseCategoryCommand request, 
        CancellationToken cancellationToken)
    {
        var courseCategory = await _unitOfWork
            .CourseCategoryRepository
            .GetByIdAsync(request.CourseCategoryId, cancellationToken);

        if (courseCategory is null)
        {
            throw new EntityNotFoundException(
                "CourseCategory",
                "Id",
                request.CourseCategoryId.ToString());
        }
        
        _courseCategoryAccessService.EnsureCanUpdate(courseCategory);
        
        courseCategory.UpdateDetails(request.Name);
        
        if (await _unitOfWork
                .CourseCategoryRepository
                .GetByNormalizedNameAsync(courseCategory.NormalizedName, cancellationToken) is not null)
        {
            throw new EntityAlreadyExistsException(
                "CourseCategory",
                "Name",
                request.Name);
        }
        
        await _unitOfWork
            .CourseCategoryRepository
            .UpdateAsync(courseCategory, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);
        
        return _mapper.Map<CourseCategoryDto>(courseCategory);
    }
}