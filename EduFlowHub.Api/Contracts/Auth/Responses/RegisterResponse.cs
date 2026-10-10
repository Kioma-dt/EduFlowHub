namespace EduFlowHub.Api.Contracts.Auth.Responses;

public sealed record RegisterResponse(
    Guid Id,
    string Username,
    UserRole Role,
    string FirstName,
    string LastName
);