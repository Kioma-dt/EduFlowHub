using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.CourseCategory.Dtos;

namespace EduFlowHub.Application.Features.CourseCategory.Queries.ListAll;

public class ListAllCourseCategoriesHandler
    : IRequestHandler<ListAllCourseCategoriesQuery, IEnumerable<CourseCategoryDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ListAllCourseCategoriesHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<CourseCategoryDto>> Handle(
        ListAllCourseCategoriesQuery request, 
        CancellationToken cancellationToken)
    {
        var courseCategories = await _unitOfWork
            .CourseCategoryRepository
            .GetAllAsync(null, null, cancellationToken);
        
        return _mapper.Map<IEnumerable<CourseCategoryDto>>(courseCategories);
    }
}