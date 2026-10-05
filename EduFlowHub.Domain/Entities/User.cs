using EduFlowHub.Domain.Entities.Abstractions;

namespace EduFlowHub.Domain.Entities;

public class User
    : Entity
{
    public string Name { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    
    public UserRole Role { get; set; }
    
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}