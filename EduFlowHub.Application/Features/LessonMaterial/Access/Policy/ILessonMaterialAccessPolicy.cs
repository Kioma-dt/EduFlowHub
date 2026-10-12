namespace EduFlowHub.Application.Features.LessonMaterial.Access.Policy;

public interface ILessonMaterialAccessPolicy
{
    bool CanView(Domain.Entities.LessonMaterial lessonMaterial, Domain.Entities.CourseEnrollment? enrollment);
    bool CanViewList(Domain.Entities.Course course, Domain.Entities.CourseEnrollment? enrollment);
    bool CanEdit(Domain.Entities.LessonMaterial lessonMaterial);
    bool CanDelete(Domain.Entities.LessonMaterial lessonMaterial);
    bool CanCreate(Guid authorId);
}