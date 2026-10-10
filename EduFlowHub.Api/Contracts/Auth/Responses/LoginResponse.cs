namespace EduFlowHub.Api.Contracts.Auth.Responses;

public sealed record LoginResponse(
    string Token,
    Guid Id,
    string Username,
    UserRole Role,
    string FirstName,
    string LastName);