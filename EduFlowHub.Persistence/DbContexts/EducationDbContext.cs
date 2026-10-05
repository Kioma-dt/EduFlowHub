using FileInfo = System.IO.FileInfo;

namespace EduFlowHub.Persistence.DbContexts;

public class EducationDbContext
    : DbContext
{
    public EducationDbContext(DbContextOptions<EducationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseCategory>  CourseCategories { get; set; }
    public DbSet<CourseEnrollment>  CourseEnrollments { get; set; }
    public DbSet<FileInfo>  FileInfos { get; set; }
    public DbSet<LessonMaterial>  LessonMaterials { get; set; }
    public DbSet<User> Users { get; set; }
}