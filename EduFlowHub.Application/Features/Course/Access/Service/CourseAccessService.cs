using EduFlowHub.Application.Features.Course.Access.Policy;

namespace EduFlowHub.Application.Features.Course.Access.Service;

public class CourseAccessService
    : ICourseAccessService
{
    private readonly ICourseAccessPolicy _courseAccessPolicy;

    public CourseAccessService(ICourseAccessPolicy courseAccessPolicy)
    {
        _courseAccessPolicy = courseAccessPolicy;
    }
    
    public void EnsureCanArchive(Domain.Entities.Course course)
    {
        if (!_courseAccessPolicy.CanArchive(course)
            || !_courseAccessPolicy.CanView(course))
        {
            throw new AccessDeniedException("Archive course");
        }
    }

    public void EnsureCanChangeAuthor(Domain.Entities.Course course)
    {
        if (!_courseAccessPolicy.CanChangeAuthor(course)
            || !_courseAccessPolicy.CanView(course))
        {
            throw new AccessDeniedException("Change course author");
        }
    }

    public void EnsureCanCreate(Guid authorId)
    {
        if (!_courseAccessPolicy.CanCreate(authorId))
        {
            throw new AccessDeniedException("Create course");
        }
    }

    public void EnsureCanDelete(Domain.Entities.Course course)
    {
        if (!_courseAccessPolicy.CanDelete(course)
            || !_courseAccessPolicy.CanView(course))
        {
            throw new AccessDeniedException("Delete course");
        }
    }

    public void EnsureCanPublish(Domain.Entities.Course course)
    {
        if (!_courseAccessPolicy.CanPublish(course)
            || !_courseAccessPolicy.CanView(course))
        {
            throw new AccessDeniedException("Publish course");
        }
    }

    public void EnsureCanRestore(Domain.Entities.Course course)
    {
        if (!_courseAccessPolicy.CanRestore(course)
            || !_courseAccessPolicy.CanView(course))
        {
            throw new AccessDeniedException("Restore course");
        }
    }

    public void EnsureCanUnpublish(Domain.Entities.Course course)
    {
        if (!_courseAccessPolicy.CanUnpublish(course)
            || !_courseAccessPolicy.CanView(course))
        {
            throw new AccessDeniedException("Unpublish course");
        }
    }

    public void EnsureCanUpdate(Domain.Entities.Course course)
    {

        if (!_courseAccessPolicy.CanEditDetails(course)
            || !_courseAccessPolicy.CanView(course))
        {
            throw new AccessDeniedException("Update course");
        }
    }
}