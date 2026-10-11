namespace EduFlowHub.Application.Features.Course.Access.Policy;

public interface ICourseAccessPolicy
{
    public bool CanView(Domain.Entities.Course course);
    public bool CanCreate(Guid authorId);
    public bool CanArchive(Domain.Entities.Course course);
    public bool CanPublish(Domain.Entities.Course course);
    public bool CanUnpublish(Domain.Entities.Course course);
    public bool CanRestore(Domain.Entities.Course course);
    public bool CanChangeAuthor(Domain.Entities.Course course);
    public bool CanEditDetails(Domain.Entities.Course course);
    public bool CanDelete(Domain.Entities.Course course);
}