using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.CourseCategory.Dtos;

namespace EduFlowHub.Application.Features.CourseCategory.Commands.UpdateOrCreate;

public class UpdateOrCreateCourseCategoryHandler
    : IRequestHandler<UpdateOrCreateCourseCategoryCommand, UpdatedOrCreatedCourseCategoryDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateOrCreateCourseCategoryHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<UpdatedOrCreatedCourseCategoryDto> Handle(
        UpdateOrCreateCourseCategoryCommand request, 
        CancellationToken cancellationToken)
    {
        var courseCategory = await _unitOfWork
            .CourseCategoryRepository
            .GetByIdAsync(request.CourseCategoryId);

        if (courseCategory is null)
        {
            var courseCategoryNew = new Domain.Entities.CourseCategory(request.Name);
            
            courseCategoryNew.SetId(request.CourseCategoryId); 
            
            Domain.Entities.CourseCategory?  courseCategoryDb = await _unitOfWork
                .CourseCategoryRepository
                .GetByNormalizedNameAsync(courseCategoryNew.NormalizedName);

            if (courseCategoryDb is not null)
            {
                throw new EntityAlreadyExistsException(
                    "CourseCategory",
                    "Name",
                    courseCategoryDb.Name);
            }
            
            await _unitOfWork
                .CourseCategoryRepository
                .InsertAsync(courseCategoryNew);
            await _unitOfWork.SaveAsync();

            var resultCreated = new UpdatedOrCreatedCourseCategoryDto(
                _mapper.Map<CourseCategoryDto>(courseCategoryNew),
                true);
            
            return resultCreated;
        }
        
        courseCategory.UpdateDetails(request.Name);
        
        await _unitOfWork
            .CourseCategoryRepository
            .UpdateAsync(courseCategory);
        await _unitOfWork.SaveAsync();
        
        var resultUpdated = new UpdatedOrCreatedCourseCategoryDto(
            _mapper.Map<CourseCategoryDto>(courseCategory),
            false);
            
        return resultUpdated;
    }
}