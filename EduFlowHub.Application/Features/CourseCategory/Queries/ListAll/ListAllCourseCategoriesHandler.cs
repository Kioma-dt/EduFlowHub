using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.CourseCategory.Dtos;
using EduFlowHub.Application.Features.Dtos;

namespace EduFlowHub.Application.Features.CourseCategory.Queries.ListAll;

public class ListAllCourseCategoriesHandler
    : IRequestHandler<ListAllCourseCategoriesQuery, PagedDto<CourseCategoryDto>>
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
    
    public async Task<PagedDto<CourseCategoryDto>> Handle(
        ListAllCourseCategoriesQuery request, 
        CancellationToken cancellationToken)
    {
        var courseCategories = await _unitOfWork
            .CourseCategoryRepository
            .GetAllAsync(null, null, request.Page,request.PageSize, cancellationToken);

        var items = _mapper.Map<IEnumerable<CourseCategoryDto>>(courseCategories);
        int totalCount = await _unitOfWork
            .CourseCategoryRepository
            .CountAsync(null, cancellationToken);
        
        return new PagedDto<CourseCategoryDto>(items, request.Page, request.PageSize, totalCount);
    }
}