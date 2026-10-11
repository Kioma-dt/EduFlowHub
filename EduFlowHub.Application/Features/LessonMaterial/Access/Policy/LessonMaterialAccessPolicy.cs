using EduFlowHub.Application.Abstractions.Services.User;

namespace EduFlowHub.Application.Features.LessonMaterial.Access.Policy;

public class LessonMaterialAccessPolicy
    : ILessonMaterialAccessPolicy
{
    private readonly ICurrentUser _currentUser;
    
    public LessonMaterialAccessPolicy(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }

    public bool CanView(Domain.Entities.LessonMaterial lessonMaterial, Domain.Entities.CourseEnrollment? enrollment)
    {
        if (lessonMaterial.Course is null)
        {
            throw new NullReferenceException(nameof(lessonMaterial.Course));
        }
        
        return _currentUser.CurrentUserRole ==  UserRole.Admin
            || (_currentUser.CurrentUserRole == UserRole.Teacher && _currentUser.CurrentUserId == lessonMaterial.Course.AuthorId)
            || (enrollment is not null 
                && _currentUser.CurrentUserRole == UserRole.Student 
                && _currentUser.CurrentUserId == enrollment.StudentId
                && lessonMaterial.Course.Id == enrollment.CourseId);
    }

    public bool CanEdit(Domain.Entities.LessonMaterial lessonMaterial)
    {
        if (lessonMaterial.Course is null)
        {
            throw new NullReferenceException(nameof(lessonMaterial.Course));
        }

        return (_currentUser.CurrentUserRole == UserRole.Admin
                || (_currentUser.CurrentUserRole == UserRole.Teacher &&
                    _currentUser.CurrentUserId == lessonMaterial.Course.AuthorId));
    }

    public bool CanDelete(Domain.Entities.LessonMaterial lessonMaterial)
    {
        if (lessonMaterial.Course is null)
        {
            throw new NullReferenceException(nameof(lessonMaterial.Course));
        }

        return (_currentUser.CurrentUserRole == UserRole.Admin
                || (_currentUser.CurrentUserRole == UserRole.Teacher &&
                    _currentUser.CurrentUserId == lessonMaterial.Course.AuthorId));
    }


    public bool CanCreate(Guid authorId)
    {
        return (_currentUser.CurrentUserRole == UserRole.Admin
                || (_currentUser.CurrentUserRole == UserRole.Teacher &&
                    _currentUser.CurrentUserId == authorId));
    }
        
}