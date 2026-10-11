namespace EduFlowHub.Application.Features.CourseCategory.Access.Service;

public interface ICourseCategoryAccessService
{
    void EnsureCanCreate();
    void EnsureCanDelete(Domain.Entities.CourseCategory courseCategory);
    void EnsureCanUpdate(Domain.Entities.CourseCategory courseCategory);
}