using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.CourseCategory.Dtos;

namespace EduFlowHub.Application.Features.CourseCategory.Queries.GetById;

public class GetCourseCategoryByIdHandler
    : IRequestHandler<GetCourseCategoryByIdQuery, CourseCategoryDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetCourseCategoryByIdHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<CourseCategoryDto> Handle(
        GetCourseCategoryByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var courseCategory = await _unitOfWork
            .CourseCategoryRepository
            .GetByIdAsync(request.CourseCategoryId, cancellationToken);

        if (courseCategory is null)
        {
            throw new EntityNotFoundException(
                "Course Category", 
                "Id",
                request.CourseCategoryId.ToString());
        }
        
        return _mapper.Map<CourseCategoryDto>(courseCategory);
    }
}