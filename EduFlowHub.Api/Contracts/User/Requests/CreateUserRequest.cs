namespace EduFlowHub.Api.Contracts.User.Requests;

public sealed record CreateUserRequest(
    string Username,
    string Password,
    UserRole Role,
    string FirstName,
    string LastName);