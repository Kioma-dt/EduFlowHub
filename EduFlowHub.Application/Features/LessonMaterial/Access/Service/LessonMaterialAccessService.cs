using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Abstractions.Services.User;
using EduFlowHub.Application.Features.LessonMaterial.Access.Policy;

namespace EduFlowHub.Application.Features.LessonMaterial.Access.Service;

public class LessonMaterialAccessService
    : ILessonMaterialAccessService
{
    private readonly ILessonMaterialAccessPolicy _lessonMaterialAccessPolicy;
    private readonly ICurrentUser _currentUser;
    private readonly IUnitOfWork _unitOfWork;

    public LessonMaterialAccessService(
        ILessonMaterialAccessPolicy lessonMaterialAccessPolicy,
        IUnitOfWork unitOfWork,
        ICurrentUser currentUser)
    {
        _lessonMaterialAccessPolicy = lessonMaterialAccessPolicy;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
    }

    public void EnsureCanChangeFile(Domain.Entities.LessonMaterial lessonMaterial)
    {
        if (!_lessonMaterialAccessPolicy.CanEdit(lessonMaterial)
            || !_lessonMaterialAccessPolicy.CanView(lessonMaterial, null))
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
        if (!_lessonMaterialAccessPolicy.CanDelete(lessonMaterial)
            || !_lessonMaterialAccessPolicy.CanView(lessonMaterial, null))
        {
            throw new AccessDeniedException("Delete lesson material");
        }
    }

    public void EnsureCanUpdate(Domain.Entities.LessonMaterial lessonMaterial)
    {
        if (!_lessonMaterialAccessPolicy.CanEdit(lessonMaterial)
            || !_lessonMaterialAccessPolicy.CanView(lessonMaterial, null))
        {
            throw new AccessDeniedException("Update lesson material");
        }
    }

    public async Task EnsureCanViewAsync(Guid courseId, CancellationToken  cancellationToken = default)
    {
        var course = await _unitOfWork.CourseRepository.GetByIdAsync(courseId, cancellationToken);
        if (course is null)
        {
            throw new EntityNotFoundException(
                "Course",
                "Id",
                courseId.ToString());
        }

        var enrollment =
            await _unitOfWork.CourseEnrollmentRepository.GetByIdAsync(_currentUser.CurrentUserId, course.Id, cancellationToken);
        if (!_lessonMaterialAccessPolicy.CanViewList(course.AuthorId, enrollment))
        {
            throw new AccessDeniedException("View lesson material");
        }
    }
}