namespace EduFlowHub.Application.Features.User.Dtos;

public sealed record UserDto(
    Guid Id,
    string Username,
    UserRole Role,
    string FirstName, 
    string LastName);