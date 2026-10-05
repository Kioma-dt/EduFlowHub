namespace EduFlowHub.Persistence.Configurations;

public class CourseConfiguration
    : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(course => course.Id);
        
        builder.Property(course => course.Title)
            .HasMaxLength(256)
            .IsRequired();
        
        builder.Property(course => course.Description)
            .HasMaxLength(1024)
            .IsRequired();
        
        builder.Property(course => course.Price)
            .HasPrecision(10, 2)
            .IsRequired();
        
        builder.Property(course => course.Status)
            .HasConversion<string>()
            .IsRequired();
        
        builder.HasOne(course => course.Category)
            .WithMany(category => category.Courses)
            .HasForeignKey(course => course.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(course => course.Author)
            .WithMany()
            .HasForeignKey(course => course.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(course => course.LessonMaterials)
            .WithOne(lesson => lesson.Course)
            .HasForeignKey(lesson => lesson.CourseId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.ToTable(table => table.HasCheckConstraint(
            "CK_Course_Price_Positive",
            "\"Price\" > 0"));
    }
}