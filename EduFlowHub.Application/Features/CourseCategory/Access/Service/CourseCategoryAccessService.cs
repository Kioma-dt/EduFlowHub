using EduFlowHub.Application.Features.CourseCategory.Access.Policy;

namespace EduFlowHub.Application.Features.CourseCategory.Access.Service;

public class CourseCategoryAccessService
    : ICourseCategoryAccessService
{
    private readonly ICourseCategoryAccessPolicy _courseCategoryAccessPolicy;

    public CourseCategoryAccessService(ICourseCategoryAccessPolicy courseCategoryAccessPolicy)
    {
        _courseCategoryAccessPolicy = courseCategoryAccessPolicy;
    }
    
    public void EnsureCanCreate()
    {
        if (!_courseCategoryAccessPolicy.CanCreate())
        {
            throw new AccessDeniedException("Create course category");
        }
    }

    public void EnsureCanDelete(Domain.Entities.CourseCategory courseCategory)
    {
        if (!_courseCategoryAccessPolicy.CanView(courseCategory))
        {
            throw new EntityNotFoundException(
                "CourseCategory",
                "Id",
                courseCategory.Id.ToString());
        }

        if (!_courseCategoryAccessPolicy.CanDelete(courseCategory))
        {
            throw new AccessDeniedException("Delete course category");
        }
    }

    public void EnsureCanUpdate(Domain.Entities.CourseCategory courseCategory)
    {
        if (!_courseCategoryAccessPolicy.CanView(courseCategory))
        {
            throw new EntityNotFoundException(
                "CourseCategory",
                "Id",
                courseCategory.Id.ToString());
        }

        if (!_courseCategoryAccessPolicy.CanEdit(courseCategory))
        {
            throw new AccessDeniedException("Update course category");
        }
    }
}