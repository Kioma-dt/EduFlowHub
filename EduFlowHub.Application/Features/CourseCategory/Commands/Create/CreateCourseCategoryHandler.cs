using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.CourseCategory.Dtos;

namespace EduFlowHub.Application.Features.CourseCategory.Commands.Create;

public class CreateCourseCategoryHandler
    : IRequestHandler<CreateCourseCategoryCommand, CourseCategoryDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCourseCategoryHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<CourseCategoryDto> Handle(
        CreateCourseCategoryCommand request, 
        CancellationToken cancellationToken)
    {
        var courseCategory = new Domain.Entities.CourseCategory(request.Name);
        
        Domain.Entities.CourseCategory?  courseCategoryDb = await _unitOfWork
            .CourseCategoryRepository
            .GetByNormalizedNameAsync(courseCategory.NormalizedName);

        if (courseCategoryDb is not null)
        {
            throw new EntityAlreadyExistsException(
                "CourseCategory",
                "Name",
                courseCategoryDb.Name);
        }
        
        await _unitOfWork
            .CourseCategoryRepository
            .InsertAsync(courseCategory);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<CourseCategoryDto>(courseCategory);
    }
}