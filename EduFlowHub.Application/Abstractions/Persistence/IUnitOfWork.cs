using EduFlowHub.Application.Abstractions.Persistence.Repositories;

namespace EduFlowHub.Application.Abstractions.Persistence;

public interface IUnitOfWork
{
    public ICourseRepository CourseRepository { get; }
    public ICourseEnrollmentRepository  CourseEnrollmentRepository { get; }
    public ICourseCategoryRepository  CourseCategoryRepository { get; }
    public IFileMetadataRepository   FileMetadataRepository { get; }
    public ILessonMaterialRepository   LessonMaterialRepository { get; }
    public IUserRepository UserRepository { get; }
    
    public Task SaveAsync();
}