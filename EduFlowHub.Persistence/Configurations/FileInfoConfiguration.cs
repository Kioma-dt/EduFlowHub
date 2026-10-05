namespace EduFlowHub.Persistence.Configurations;

public class FileMetadataConfiguration
    : IEntityTypeConfiguration<FileMetadata>
{

    public void Configure(EntityTypeBuilder<FileMetadata> builder)
    {
        builder.HasKey(fileMetadata  => fileMetadata.Id);
        
        builder.Property(fileMetadata => fileMetadata.Path)
            .HasMaxLength(256)
            .IsRequired();
        
        builder.Property(fileMetadata => fileMetadata.Extension)
            .HasMaxLength(16)
            .IsRequired();
        
        builder.Property(fileMetadata => fileMetadata.Size)
            .IsRequired();
        
        builder.HasIndex(fileMetadata => fileMetadata.Path)
            .IsUnique();
    }
}