using FileInfo = EduFlowHub.Domain.Entities.FileInfo;

namespace EduFlowHub.Persistence.Configurations;

public class FileInfoConfiguration
    : IEntityTypeConfiguration<FileInfo>
{

    public void Configure(EntityTypeBuilder<FileInfo> builder)
    {
        builder.HasKey(fileInfo => fileInfo.Id);
        
        builder.Property(fileInfo => fileInfo.Path)
            .HasMaxLength(256)
            .IsRequired();
        
        builder.Property(fileInfo => fileInfo.Extension)
            .HasMaxLength(16)
            .IsRequired();
        
        builder.Property(fileInfo => fileInfo.Size)
            .IsRequired();
        
        builder.HasIndex(fileInfo => fileInfo.Path)
            .IsUnique();
    }
}