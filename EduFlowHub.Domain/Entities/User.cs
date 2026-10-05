namespace EduFlowHub.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}