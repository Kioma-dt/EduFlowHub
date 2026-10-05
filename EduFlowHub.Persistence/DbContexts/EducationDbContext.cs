using EduFlowHub.Persistence.Configurations;
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
    public DbSet<FileMetadata>  FileMetadata { get; set; }
    public DbSet<LessonMaterial>  LessonMaterials { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new CourseConfiguration());
        builder.ApplyConfiguration(new CourseCategoryConfiguration());
        builder.ApplyConfiguration(new CourseEnrollmentConfiguration());
        builder.ApplyConfiguration(new FileMetadataConfiguration());
        builder.ApplyConfiguration(new LessonMaterialConfiguration());
        builder.ApplyConfiguration(new UserConfiguration());
        
        base.OnModelCreating(builder);
    }
}