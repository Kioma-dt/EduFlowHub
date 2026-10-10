namespace EduFlowHub.Api.Contracts.Auth.Requests;

public sealed record RegisterRequest(
    string Username,
    string Password,
    UserRole Role,
    string FirstName,
    string LastName);