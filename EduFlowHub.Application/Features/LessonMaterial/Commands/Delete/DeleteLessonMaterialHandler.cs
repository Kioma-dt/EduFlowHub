using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Abstractions.Services.File;
using EduFlowHub.Application.Features.LessonMaterial.Access.Service;

namespace EduFlowHub.Application.Features.LessonMaterial.Commands.Delete;

public class DeleteLessonMaterialHandler
    : IRequestHandler<DeleteLessonMaterialCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorage _fileStorage;
    private readonly ILessonMaterialAccessService _lessonMaterialAccessService;

    public DeleteLessonMaterialHandler(
        IUnitOfWork unitOfWork,
        IFileStorage fileStorage,
        ILessonMaterialAccessService lessonMaterialAccessService)
    {
        _unitOfWork = unitOfWork;
        _fileStorage = fileStorage;
        _lessonMaterialAccessService = lessonMaterialAccessService;
    }

    public async Task Handle(
        DeleteLessonMaterialCommand request, 
        CancellationToken cancellationToken)
    {
        Domain.Entities.LessonMaterial? lessonMaterial = await _unitOfWork
            .LessonMaterialRepository
            .GetByIdAsync(request.LessonMaterialId,
                cancellationToken,
                lessonMaterial => lessonMaterial.FileMetadata!,
                lessonMaterial => lessonMaterial.Course!);

        if (lessonMaterial is null
            || lessonMaterial.CourseId != request.CourseId)
        {
            throw new EntityNotFoundException(
                "LessonMaterial",
                "Id",
                request.LessonMaterialId.ToString());
        }
        
        _lessonMaterialAccessService.EnsureCanDelete(lessonMaterial);
        
        lessonMaterial.Course?.EnsureCanChange();
        
        string filePath = lessonMaterial.FileMetadata!.Path;
        
        await _unitOfWork
            .LessonMaterialRepository
            .DeleteAsync(lessonMaterial, cancellationToken);
        
        await _unitOfWork.SaveAsync(cancellationToken);
        
        await _fileStorage.DeleteAsync(filePath, cancellationToken);
    }
}