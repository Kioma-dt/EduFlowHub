namespace EduFlowHub.Persistence.Configurations;

public class CourseEnrollmentConfiguration
    : IEntityTypeConfiguration<CourseEnrollment>
{
    public void Configure(EntityTypeBuilder<CourseEnrollment> builder)
    {
        builder.HasKey(courseEnrollment => 
            new { courseEnrollment.CourseId, courseEnrollment.UserId });

        builder.HasOne(courseEnrollment => courseEnrollment.Course)
            .WithMany()
            .HasForeignKey(courseEnrollment => courseEnrollment.CourseId);
        
        builder.HasOne(courseEnrollment => courseEnrollment.User)
            .WithMany()
            .HasForeignKey(courseEnrollment => courseEnrollment.UserId);
    }
}