using EduFlowHub.Domain.Entities.Abstractions;
using EduFlowHub.Domain.Regexes;

namespace EduFlowHub.Domain.Entities;

public class User
    : Entity
{
    public User()
    {
    }

    public User(
        string name,
        string passwordHash,
        UserRole role,
        string firstName,
        string lastName,
        string? email,
        string? phoneNumber)
    {
        Name = ValidateName(name);
        PasswordHash = ValidatePasswordHash(passwordHash);
        Role = role;
        FirstName = ValidateFirstName(firstName);
        LastName = ValidateLastName(lastName);
        Email = email is not null 
            ? ValidateEmail(email)
            : Email;
        PhoneNumber = phoneNumber is not null 
            ? ValidatePhoneNumber(phoneNumber)
            : PhoneNumber;
    }

    public void UpdateDetails(
        string firstName,
        string lastName,
        string? email,
        string? phoneNumber)
    {
        FirstName = ValidateFirstName(firstName);
        LastName = ValidateLastName(lastName);
        Email = email is not null 
            ? ValidateEmail(email)
            : Email;
        PhoneNumber = phoneNumber is not null 
            ? ValidatePhoneNumber(phoneNumber)
            : PhoneNumber;
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
    
    public string? Email { get; private set; }
    public string? PhoneNumber { get; private set; }
    
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
        
        return passwordHash.Trim();
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

    private string ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new DomainValidationException("User email cannot be empty");
        }

        if (!EmailRegex.IsMatch(email))
        {
            throw new DomainValidationException("User email should be in correct format");
        }
        
        return email.Trim();
    }
    
    private string ValidatePhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            throw new DomainValidationException("User phone number cannot be empty");
        }

        if (!PhoneNumberRegex.IsMatch(phoneNumber))
        {
            throw new DomainValidationException("User phone number should be in correct format");
        }
        
        return phoneNumber.Trim();
    }
}