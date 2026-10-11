using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.CourseCategory.Access.Service;
using EduFlowHub.Application.Features.CourseCategory.Dtos;

namespace EduFlowHub.Application.Features.CourseCategory.Commands.Create;

public class CreateCourseCategoryHandler
    : IRequestHandler<CreateCourseCategoryCommand, CourseCategoryDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseCategoryAccessService _courseCategoryAccessService;

    public CreateCourseCategoryHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        ICourseCategoryAccessService courseCategoryAccessService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _courseCategoryAccessService = courseCategoryAccessService;
    }
    
    public async Task<CourseCategoryDto> Handle(
        CreateCourseCategoryCommand request, 
        CancellationToken cancellationToken)
    {
        var courseCategory = new Domain.Entities.CourseCategory(request.Name);
        
        if (await _unitOfWork
                .CourseCategoryRepository
                .GetByNormalizedNameAsync(courseCategory.NormalizedName, cancellationToken) is not null)
        {
            throw new EntityAlreadyExistsException(
                "CourseCategory",
                "Name",
                request.Name);
        }
        
        _courseCategoryAccessService.EnsureCanCreate(courseCategory);
        
        await _unitOfWork
            .CourseCategoryRepository
            .InsertAsync(courseCategory, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        return _mapper.Map<CourseCategoryDto>(courseCategory);
    }
}