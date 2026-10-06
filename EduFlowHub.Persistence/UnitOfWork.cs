using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Abstractions.Persistence.Repositories;

namespace EduFlowHub.Persistence;

public class UnitOfWork
    : IUnitOfWork,
        IDisposable
{
    private readonly DbContext _dbContext;
    
    private readonly ICourseRepository _courseRepository;
    private readonly ICourseEnrollmentRepository _courseEnrollmentRepository;
    private readonly ICourseCategoryRepository _courseCategoryRepository;
    private readonly IFileMetadataRepository _fileMetadataRepository;
    private readonly ILessonMaterialRepository _lessonMaterialRepository;
    private readonly IUserRepository _userRepository;

    public UnitOfWork(
        DbContext dbContext,
        ICourseRepository courseRepository,
        ICourseEnrollmentRepository courseEnrollmentRepository,
        ICourseCategoryRepository courseCategoryRepository,
        IFileMetadataRepository fileMetadataRepository,
        ILessonMaterialRepository lessonMaterialRepository,
        IUserRepository userRepository)
    {
        _dbContext = dbContext;
        
        _courseRepository = courseRepository;
        _courseEnrollmentRepository = courseEnrollmentRepository;
        _courseCategoryRepository = courseCategoryRepository;
        _fileMetadataRepository = fileMetadataRepository;
        _lessonMaterialRepository = lessonMaterialRepository;
        _userRepository = userRepository;
    }

    public ICourseRepository CourseRepository => _courseRepository;
    public ICourseEnrollmentRepository CourseEnrollmentRepository => _courseEnrollmentRepository;
    public ICourseCategoryRepository CourseCategoryRepository => _courseCategoryRepository;
    public IFileMetadataRepository FileMetadataRepository => _fileMetadataRepository;
    public ILessonMaterialRepository LessonMaterialRepository => _lessonMaterialRepository;
    public IUserRepository UserRepository => _userRepository;
    
    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    private bool _disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this._disposed)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
        this._disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}