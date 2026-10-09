using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Abstractions.Services;

namespace EduFlowHub.Application.Features.LessonMaterial.Commands.Delete;

public class DeleteLessonMaterialHandler
    : IRequestHandler<DeleteLessonMaterialCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorage _fileStorage;

    public DeleteLessonMaterialHandler(
        IUnitOfWork unitOfWork,
        IFileStorage fileStorage)
    {
        _unitOfWork = unitOfWork;
        _fileStorage = fileStorage;
    }

    public async Task Handle(
        DeleteLessonMaterialCommand request, 
        CancellationToken cancellationToken)
    {
        Domain.Entities.LessonMaterial? lessonMaterial = await _unitOfWork
            .LessonMaterialRepository
            .GetByIdAsync(request.LessonMaterialId,
                lessonMaterial => lessonMaterial.FileMetadata!);

        if (lessonMaterial is null)
        {
            throw new EntityNotFoundException(
                "LessonMaterial",
                "Id",
                request.LessonMaterialId.ToString());
        }
        
        string filePath = lessonMaterial.FileMetadata!.Path;
        
        await _unitOfWork
            .LessonMaterialRepository
            .DeleteAsync(lessonMaterial);
        
        await _unitOfWork.SaveAsync();
        
        await _fileStorage.DeleteAsync(filePath);
    }
}