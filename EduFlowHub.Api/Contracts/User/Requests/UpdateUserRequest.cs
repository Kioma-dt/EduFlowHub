namespace EduFlowHub.Api.Contracts.User.Requests;

public sealed record UpdateUserRequest(
    string? FirstName,
    string? LastName);