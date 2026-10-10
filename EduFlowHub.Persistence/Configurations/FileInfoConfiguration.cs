namespace EduFlowHub.Persistence.Configurations;

public class FileMetadataConfiguration
    : IEntityTypeConfiguration<FileMetadata>
{

    public void Configure(EntityTypeBuilder<FileMetadata> builder)
    {
        builder.HasKey(fileMetadata  => fileMetadata.Id);
        
        builder.Property(fileMetadata => fileMetadata.Path)
            .HasMaxLength(128)
            .IsRequired();
        
        builder.Property(fileMetadata => fileMetadata.Extension)
            .HasMaxLength(16)
            .IsRequired();
        
        builder.Property(fileMetadata => fileMetadata.Size)
            .IsRequired();
        
        builder.HasOne(fileMetadata => fileMetadata.LessonMaterial)
            .WithOne(lessonMaterial => lessonMaterial.FileMetadata)
            .HasForeignKey<FileMetadata>(fileMetadata => fileMetadata.LessonMaterialId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(fileMetadata => fileMetadata.Path)
            .IsUnique();
    }
}