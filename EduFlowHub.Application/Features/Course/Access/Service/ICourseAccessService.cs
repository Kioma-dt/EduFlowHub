namespace EduFlowHub.Application.Features.Course.Access.Service;

public interface ICourseAccessService
{
    void EnsureCanArchive(Domain.Entities.Course course);
    void EnsureCanChangeAuthor(Domain.Entities.Course course);
    void EnsureCanCreate(Guid authorId);
    void EnsureCanDelete(Domain.Entities.Course course);
    void EnsureCanPublish(Domain.Entities.Course course);
    void EnsureCanRestore(Domain.Entities.Course course);
    void EnsureCanUnpublish(Domain.Entities.Course course);
    void EnsureCanUpdate(Domain.Entities.Course course);
}