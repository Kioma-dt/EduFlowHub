using EduFlowHub.Application.Features.LessonMaterial.Access.Policy;

namespace EduFlowHub.Application.Features.LessonMaterial.Access.Service;

public class LessonMaterialAccessService
    : ILessonMaterialAccessService
{
    private readonly ILessonMaterialAccessPolicy _lessonMaterialAccessPolicy;

    public LessonMaterialAccessService(ILessonMaterialAccessPolicy lessonMaterialAccessPolicy)
    {
        _lessonMaterialAccessPolicy = lessonMaterialAccessPolicy;
    }
    
    public void EnsureCanChangeFile(Domain.Entities.LessonMaterial lessonMaterial)
    {
        if (!_lessonMaterialAccessPolicy.CanView(lessonMaterial, null))
        {
            throw new EntityNotFoundException(
                "LessonMaterial",
                "Id",
                lessonMaterial.Id.ToString());
        }

        if (!_lessonMaterialAccessPolicy.CanEdit(lessonMaterial))
        {
            throw new AccessDeniedException("Change lesson material file");
        }
    }

    public void EnsureCanCreate(Guid authorId)
    {
        if (!_lessonMaterialAccessPolicy.CanCreate(authorId))
        {
            throw new AccessDeniedException("Create lesson material");
        }
    }

    public void EnsureCanDelete(Domain.Entities.LessonMaterial lessonMaterial)
    {
        if (!_lessonMaterialAccessPolicy.CanView(lessonMaterial, null))
        {
            throw new EntityNotFoundException(
                "LessonMaterial",
                "Id",
                lessonMaterial.Id.ToString());
        }

        if (!_lessonMaterialAccessPolicy.CanDelete(lessonMaterial))
        {
            throw new AccessDeniedException("Delete lesson material");
        }
    }

    public void EnsureCanUpdate(Domain.Entities.LessonMaterial lessonMaterial)
    {
        if (!_lessonMaterialAccessPolicy.CanView(lessonMaterial, null))
        {
            throw new EntityNotFoundException(
                "LessonMaterial",
                "Id",
                lessonMaterial.Id.ToString());
        }

        if (!_lessonMaterialAccessPolicy.CanEdit(lessonMaterial))
        {
            throw new AccessDeniedException("Update lesson material");
        }
    }
}