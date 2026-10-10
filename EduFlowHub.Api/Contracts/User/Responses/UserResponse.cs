namespace EduFlowHub.Api.Contracts.User.Responses;

public sealed record UserResponse(
    Guid Id,
    string Username,
    UserRole Role,
    string FirstName, 
    string LastName);