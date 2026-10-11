using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Abstractions.Services.File;
using EduFlowHub.Application.Features.LessonMaterial.Access.Service;
using EduFlowHub.Application.Features.LessonMaterial.FileValidator;

namespace EduFlowHub.Application.Features.LessonMaterial.Commands.ChangeFile;

public class ChangeLessonMaterialFileHandler
    : IRequestHandler<ChangeLessonMaterialFileCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorage _fileStorage;
    private readonly ILessonMaterialAccessService _lessonMaterialAccessService;
    private readonly IFileValidator _fileValidator;
    

    public ChangeLessonMaterialFileHandler(
        IUnitOfWork unitOfWork,
        IFileStorage fileStorage,
        ILessonMaterialAccessService lessonMaterialAccessService,
        IFileValidator fileValidator)
    {
        _unitOfWork = unitOfWork;
        _fileStorage = fileStorage;
        _lessonMaterialAccessService = lessonMaterialAccessService;
        _fileValidator = fileValidator;
    }

    public async Task Handle(
        ChangeLessonMaterialFileCommand request, 
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
        
        _lessonMaterialAccessService.EnsureCanChangeFile(lessonMaterial);

        lessonMaterial.Course?.EnsureCanChange();
        
        _fileValidator.ValidateFile(request.FileStream, request.FileExtension, request.FileLength);
        
        string filePathOld = lessonMaterial.FileMetadata!.Path;

        long fileSize = request.FileStream.Length;
        string filePath = await _fileStorage.UploadAsync(
            request.FileStream,
            request.FileExtension,
            cancellationToken);

        FileMetadata fileMetadata = new FileMetadata(filePath, request.FileExtension, fileSize);
        
        MaterialType materialType = MaterialTypeResolver.FromExtension(request.FileExtension);

        lessonMaterial.ChangeFile(fileMetadata, materialType);

        await _unitOfWork
            .LessonMaterialRepository
            .UpdateAsync(lessonMaterial, cancellationToken);
        
        await _unitOfWork.SaveAsync(cancellationToken);
        
        await _fileStorage.DeleteAsync(filePathOld, cancellationToken);
    }
}