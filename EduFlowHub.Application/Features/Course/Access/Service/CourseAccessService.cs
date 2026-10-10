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
        if (!_courseAccessPolicy.CanView(course))
        {
            throw new EntityNotFoundException(
                "Course",
                "Id",
                course.Id.ToString());
        }

        if (!_courseAccessPolicy.CanArchive(course))
        {
            throw new AccessDeniedException("Archive course");
        }
    }

    public void EnsureCanChangeAuthor(Domain.Entities.Course course)
    {
        if (!_courseAccessPolicy.CanView(course))
        {
            throw new EntityNotFoundException(
                "Course",
                "Id",
                course.Id.ToString());
        }

        if (!_courseAccessPolicy.CanArchive(course))
        {
            throw new AccessDeniedException("Change course author");
        }
    }

    public void EnsureCanCreate(Domain.Entities.Course course)
    {
        if (!_courseAccessPolicy.CanCreate(course))
        {
            throw new AccessDeniedException("Create course");
        }
    }

    public void EnsureCanDelete(Domain.Entities.Course course)
    {
        if (!_courseAccessPolicy.CanView(course))
        {
            throw new EntityNotFoundException(
                "Course",
                "Id",
                course.Id.ToString());
        }

        if (!_courseAccessPolicy.CanDelete(course))
        {
            throw new AccessDeniedException("Delete course");
        }
    }

    public void EnsureCanPublish(Domain.Entities.Course course)
    {
        if (!_courseAccessPolicy.CanView(course))
        {
            throw new EntityNotFoundException(
                "Course",
                "Id",
                course.Id.ToString());
        }

        if (!_courseAccessPolicy.CanPublish(course))
        {
            throw new AccessDeniedException("Publish course");
        }
    }

    public void EnsureCanRestore(Domain.Entities.Course course)
    {
        if (!_courseAccessPolicy.CanView(course))
        {
            throw new EntityNotFoundException(
                "Course",
                "Id",
                course.Id.ToString());
        }

        if (!_courseAccessPolicy.CanRestore(course))
        {
            throw new AccessDeniedException("Restore course");
        }
    }

    public void EnsureCanUnpublish(Domain.Entities.Course course)
    {
        if (!_courseAccessPolicy.CanView(course))
        {
            throw new EntityNotFoundException(
                "Course",
                "Id",
                course.Id.ToString());
        }

        if (!_courseAccessPolicy.CanUnpublish(course))
        {
            throw new AccessDeniedException("Unpublish course");
        }
    }

    public void EnsureCanUpdate(Domain.Entities.Course course)
    {
        if (!_courseAccessPolicy.CanView(course))
        {
            throw new EntityNotFoundException(
                "Course",
                "Id",
                course.Id.ToString());
        }

        if (!_courseAccessPolicy.CanEditDetails(course))
        {
            throw new AccessDeniedException("Update course");
        }
    }
}