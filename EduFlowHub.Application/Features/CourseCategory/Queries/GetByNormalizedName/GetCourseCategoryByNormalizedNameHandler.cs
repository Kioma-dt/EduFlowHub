using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.CourseCategory.Dtos;

namespace EduFlowHub.Application.Features.CourseCategory.Queries.GetByNormalizedName;

public class GetCourseCategoryByNormalizedNameHandler
    : IRequestHandler<GetCourseCategoryByNormalizedNameQuery, CourseCategoryDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetCourseCategoryByNormalizedNameHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<CourseCategoryDto> Handle(
        GetCourseCategoryByNormalizedNameQuery request, 
        CancellationToken cancellationToken)
    {
        var courseCategory = await _unitOfWork
            .CourseCategoryRepository
            .GetByNormalizedNameAsync(request.NormalizedName);

        if (courseCategory is null)
        {
            throw new EntityNotFoundException(
                "Course Category", 
                "NormalizedName",
                request.NormalizedName);
        }
        
        return _mapper.Map<CourseCategoryDto>(courseCategory);
    }
}