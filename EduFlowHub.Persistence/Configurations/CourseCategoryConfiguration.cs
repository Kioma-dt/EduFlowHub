namespace EduFlowHub.Persistence.Configurations;

public class CourseCategoryConfiguration
    : IEntityTypeConfiguration<CourseCategory>
{
    public void Configure(EntityTypeBuilder<CourseCategory> builder)
    {
        builder.HasKey(courseCategory => courseCategory.Id);
        
        builder.Property(courseCategory => courseCategory.Name)
            .HasMaxLength(256)
            .IsRequired();
        
        builder.Property(courseCategory => courseCategory.NormalizedName)
            .HasMaxLength(256)
            .IsRequired();
        
        builder.HasMany(courseCategory => courseCategory.Courses)
            .WithOne(course => course.Category)
            .HasForeignKey(course => course.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasIndex(course => course.NormalizedName)
            .IsUnique();
    }
}