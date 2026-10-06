using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Abstractions.Persistence.Repositories;

namespace EduFlowHub.Persistence;

public class UnitOfWork
    : IUnitOfWork,
        IDisposable
{
    public ICourseRepository CourseRepository { get; }
    public ICourseEnrollmentRepository CourseEnrollmentRepository { get; }
    public ICourseCategoryRepository CourseCategoryRepository { get; }
    public IFileMetadataRepository FileMetadataRepository { get; }
    public ILessonMaterialRepository LessonMaterialRepository { get; }
    public IUserRepository UserRepository { get; }
    public async Task SaveAsync()
    {
        
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}