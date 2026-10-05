namespace EduFlowHub.Persistence.Configurations;

public class LessonMaterialConfiguration
    : IEntityTypeConfiguration<LessonMaterial>
{

    public void Configure(EntityTypeBuilder<LessonMaterial> builder)
    {
        builder.HasKey(lessonMaterial => lessonMaterial.Id);
        
        builder.Property(lessonMaterial => lessonMaterial.Title)
            .HasMaxLength(256)
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
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(lessonMaterial => lessonMaterial.FileInfo)
            .WithMany()
            .HasForeignKey(lessonMaterial => lessonMaterial.FileInfoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(lessonMaterial
            => new { lessonMaterial.OrderNumber, lessonMaterial.CourseId })
            .IsUnique();

        builder.ToTable( table => table.HasCheckConstraint(
            "CK_LessonMaterial_OrderNumber_Positive", 
            "\"OrderNumber\" > 0"));
    }
}