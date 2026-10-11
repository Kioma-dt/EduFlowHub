using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.LessonMaterial.Access.Service;
using EduFlowHub.Application.Features.LessonMaterial.Dtos;
using EduFlowHub.Domain.Exceptions;

namespace EduFlowHub.Application.Features.LessonMaterial.Commands.Update;

public class UpdateLessonMaterialHandler
    : IRequestHandler<UpdateLessonMaterialCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILessonMaterialAccessService _lessonMaterialAccessService;

    public UpdateLessonMaterialHandler(
        IUnitOfWork unitOfWork,
        ILessonMaterialAccessService lessonMaterialAccessService)
    {
        _unitOfWork = unitOfWork;
        _lessonMaterialAccessService =  lessonMaterialAccessService;
    }

    public async Task Handle(
        UpdateLessonMaterialCommand request, 
        CancellationToken cancellationToken)
    {
        Domain.Entities.LessonMaterial? lessonMaterial = await _unitOfWork
            .LessonMaterialRepository
            .GetByIdAsync(request.LessonMaterialId,
                cancellationToken,
                lessonMaterial => lessonMaterial.FileMetadata!,
                lessonMaterial => lessonMaterial.Course!);

        if (lessonMaterial is null)
        {
            throw new EntityNotFoundException(
                "LessonMaterial",
                "Id",
                request.LessonMaterialId.ToString());
        }
        
        _lessonMaterialAccessService.EnsureCanUpdate(lessonMaterial);
        
        if (lessonMaterial.Course!.Status != CourseStatus.Draft)
        {
            throw new DomainWrongStatusException("Can not change not draft course");
        }
        
        lessonMaterial.UpdateDetails(
            request.Title ??  lessonMaterial.Title,
            request.OrderNumber ??   lessonMaterial.OrderNumber);
        
        await _unitOfWork
            .LessonMaterialRepository
            .UpdateAsync(lessonMaterial, cancellationToken);
        
        await _unitOfWork.SaveAsync(cancellationToken);
    }
}