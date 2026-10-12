using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Abstractions.Services.User;
using EduFlowHub.Application.Features.LessonMaterial.Access.Service;
using EduFlowHub.Application.Features.LessonMaterial.Dtos;

namespace EduFlowHub.Application.Features.LessonMaterial.Queries.GetById;

public class GetLessonMaterialByIdHandler
    : IRequestHandler<GetLessonMaterialByIdQuery, LessonMaterialDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILessonMaterialAccessService _lessonMaterialAccessService;

    public GetLessonMaterialByIdHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        ICurrentUser currentUser,
        ILessonMaterialAccessService lessonMaterialAccessService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _lessonMaterialAccessService = lessonMaterialAccessService;
    }

    public async Task<LessonMaterialDto> Handle(
        GetLessonMaterialByIdQuery request,
        CancellationToken cancellationToken)
    {
        await _lessonMaterialAccessService.EnsureCanViewAsync(request.CourseId, cancellationToken);
        
        Domain.Entities.LessonMaterial? lessonMaterial = await _unitOfWork
            .LessonMaterialRepository
            .GetByIdAsync(
                request.LessonMaterialId,
                cancellationToken,
                lessonMaterial => lessonMaterial.FileMetadata!);
        
        if (lessonMaterial is null)
        {
            throw new EntityNotFoundException(
                "LessonMaterial",
                "Id",
                request.LessonMaterialId.ToString());
        }
        
        if (lessonMaterial is null || lessonMaterial.CourseId != request.CourseId)
        {
            throw new EntityNotFoundException("LessonMaterial", "Id", request.LessonMaterialId.ToString());
        }
        
        return _mapper.Map<LessonMaterialDto>(lessonMaterial);
    }
}