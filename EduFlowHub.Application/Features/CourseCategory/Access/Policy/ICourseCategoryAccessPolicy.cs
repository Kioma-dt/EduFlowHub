namespace EduFlowHub.Application.Features.CourseCategory.Access.Policy;

public interface ICourseCategoryAccessPolicy
{
    bool CanView(Domain.Entities.CourseCategory courseCategory);
    bool CanEdit(Domain.Entities.CourseCategory courseCategory);
    bool CanDelete(Domain.Entities.CourseCategory courseCategory);
    bool CanCreate(Domain.Entities.CourseCategory courseCategory);
}