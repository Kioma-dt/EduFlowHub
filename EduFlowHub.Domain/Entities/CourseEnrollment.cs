namespace EduFlowHub.Domain.Entities;

public class CourseEnrollment
{
    public Guid CourseId { get; set; }
    public Guid UserId { get; set; }
    
    public Course? Course { get; set; }
    public User? User { get; set; }
}