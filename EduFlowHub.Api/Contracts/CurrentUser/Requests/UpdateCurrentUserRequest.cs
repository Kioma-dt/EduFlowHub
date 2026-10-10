namespace EduFlowHub.Api.Contracts.CurrentUser.Requests;

public sealed record UpdateCurrentUserRequest(
    string? FirstName,
    string? LastName);