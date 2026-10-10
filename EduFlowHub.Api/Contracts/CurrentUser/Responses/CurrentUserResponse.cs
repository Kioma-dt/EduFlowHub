namespace EduFlowHub.Api.Contracts.CurrentUser.Responses;

public sealed record CurrentUserResponse(
    Guid  UserId,
    string Username,
    UserRole  Role,
    string FirstName,
    string LastName);