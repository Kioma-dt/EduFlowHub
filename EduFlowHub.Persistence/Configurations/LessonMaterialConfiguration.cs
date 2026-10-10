namespace EduFlowHub.Persistence.Configurations;

public class LessonMaterialConfiguration
    : IEntityTypeConfiguration<LessonMaterial>
{

    public void Configure(EntityTypeBuilder<LessonMaterial> builder)
    {
        builder.HasKey(lessonMaterial => lessonMaterial.Id);
        
        builder.Property(lessonMaterial => lessonMaterial.Title)
            .HasMaxLength(128)
            .IsRequired();
        
        builder.Property(lessonMaterial => lessonMaterial.OrderNumber)
            .IsRequired();
        
        builder.Property(lessonMaterial => lessonMaterial.Type)
            .HasConversion<string>()
            .IsRequired();
        
        builder.Property(lessonMaterial => lessonMaterial.CreatedAt)
            .IsRequired();
        
        builder.HasOne(lessonMaterial => lessonMaterial.Course)
            .WithMany(lesson => lesson.LessonMaterials)
            .HasForeignKey(lessonMaterial => lessonMaterial.CourseId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(lessonMaterial => lessonMaterial.FileMetadata)
            .WithOne(fileMetadata => fileMetadata.LessonMaterial)
            .HasForeignKey<FileMetadata>(fileMetadata => fileMetadata.LessonMaterialId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(lessonMaterial
            => new { lessonMaterial.OrderNumber, lessonMaterial.CourseId })
            .IsUnique();

        builder.ToTable( table => table.HasCheckConstraint(
            "CK_LessonMaterial_OrderNumber_Positive", 
            "\"OrderNumber\" > 0"));
    }
}