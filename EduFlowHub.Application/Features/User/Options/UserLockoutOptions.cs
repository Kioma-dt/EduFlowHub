namespace EduFlowHub.Application.Features.User.Options;

public class UserLockoutOptions
{
    public const string SectionName = "Lockout";

    public int MaxFailedAttempts { get; set; }

    public int LockoutMinutes { get; set; } 
}