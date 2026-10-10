using EduFlowHub.Application.Abstractions.Services;

namespace EduFlowHub.Infrastructure.AuthenticationServices.PasswordHasher;

public class BCryptPasswordHasher
    : IPasswordHasher
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyHashedPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}