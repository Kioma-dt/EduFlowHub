namespace EduFlowHub.Api.Contracts.CurrentUser.Requests;

public sealed record ChangeCurrentUserPasswordRequest(
    string Password,
    string OldPassword);