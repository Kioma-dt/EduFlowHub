namespace EduFlowHub.Persistence.Configurations;

public class UserConfiguration
    : IEntityTypeConfiguration<User>
{

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);
        
        builder.Property(user => user.Name)
            .HasMaxLength(256)
            .IsRequired();
        
        builder.Property(user => user.Role)
            .HasConversion<string>()
            .IsRequired();
        
        builder.Property(user => user.PasswordHash)
            .HasMaxLength(256)
            .IsRequired();
        
        builder.Property(user => user.FirstName)
            .HasMaxLength(256)
            .IsRequired();
        
        builder.Property(user => user.LastName)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(user => user.Email)
            .HasMaxLength(256);
        
        builder.Property(user => user.Phone)
            .HasMaxLength(256);

        builder.HasIndex(user => user.Name)
            .IsUnique();
    }
}