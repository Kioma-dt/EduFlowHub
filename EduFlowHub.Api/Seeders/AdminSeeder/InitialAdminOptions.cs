namespace EduFlowHub.Api.Seeders.AdminSeeder;

public class InitialAdminOptions
{
    public static string SectionName => "InitialAdmin";

    public string? Username { get; init; }
    public string? Password { get; init; }
    public string FirstName { get; init; } = "Admin";
    public string LastName { get; init; } = "Admin";
}