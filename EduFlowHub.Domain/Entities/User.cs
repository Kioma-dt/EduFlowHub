using EduFlowHub.Domain.Entities.Abstractions;
using EduFlowHub.Domain.Regexes;

namespace EduFlowHub.Domain.Entities;

public class User
    : Entity
{
    private User()
    {
    }

    public User(
        string name,
        string passwordHash,
        UserRole role,
        string firstName,
        string lastName)
    {
        Name = ValidateName(name);
        PasswordHash = ValidatePasswordHash(passwordHash);
        Role = role;
        FirstName = ValidateFirstName(firstName);
        LastName = ValidateLastName(lastName);
    }

    public void UpdateDetails(
        string firstName,
        string lastName)
    {
        FirstName = ValidateFirstName(firstName);
        LastName = ValidateLastName(lastName);
    }

    public void ChangeName(string name)
    {
        Name = ValidateName(name);
    }

    public void ChangePasswordHash(string passwordHash)
    {
        PasswordHash = ValidatePasswordHash(passwordHash);
    }

    public void ChangeRole(UserRole role)
    {
        Role = role;
    }
    
    public string Name { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    
    public UserRole Role { get; private set; }
    
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    
    private string ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainValidationException("User name cannot be empty");
        }
        
        return name.Trim();
    }
    
    private string ValidatePasswordHash(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
        {
            throw new DomainValidationException("User password hash cannot be empty");
        }
        
        return passwordHash;
    }
    
    private string ValidateFirstName(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new DomainValidationException("User first name cannot be empty");
        }
        return firstName.Trim();
    }
    
    private string ValidateLastName(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new DomainValidationException("User last name cannot be empty");
        }
        return lastName.Trim();
    }
}