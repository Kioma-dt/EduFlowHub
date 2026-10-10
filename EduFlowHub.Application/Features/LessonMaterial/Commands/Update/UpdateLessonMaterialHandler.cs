using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.LessonMaterial.Dtos;

namespace EduFlowHub.Application.Features.LessonMaterial.Commands.Update;

public class UpdateLessonMaterialHandler
    : IRequestHandler<UpdateLessonMaterialCommand, LessonMaterialDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateLessonMaterialHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<LessonMaterialDto> Handle(
        UpdateLessonMaterialCommand request, 
        CancellationToken cancellationToken)
    {
        Domain.Entities.LessonMaterial? lessonMaterial = await _unitOfWork
            .LessonMaterialRepository
            .GetByIdAsync(request.LessonMaterialId,
                cancellationToken,
                lessonMaterial => lessonMaterial.FileMetadata!);

        if (lessonMaterial is null)
        {
            throw new EntityNotFoundException(
                "LessonMaterial",
                "Id",
                request.LessonMaterialId.ToString());
        }
        
        lessonMaterial.UpdateDetails(
            request.Title ??  lessonMaterial.Title,
            request.OrderNumber ??   lessonMaterial.OrderNumber);
        
        await _unitOfWork
            .LessonMaterialRepository
            .UpdateAsync(lessonMaterial, cancellationToken);
        
        await _unitOfWork.SaveAsync(cancellationToken);

        Domain.Entities.LessonMaterial lessonMaterialWithNavigations = await _unitOfWork
            .LessonMaterialRepository
            .GetByIdAsync(
                lessonMaterial.Id,
                cancellationToken,
                lessonMaterial => lessonMaterial.FileMetadata);
        
        return _mapper.Map<LessonMaterialDto>(lessonMaterialWithNavigations);
    }
}