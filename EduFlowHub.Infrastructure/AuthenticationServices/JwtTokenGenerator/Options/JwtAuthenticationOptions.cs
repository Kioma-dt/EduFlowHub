namespace EduFlowHub.Infrastructure.AuthenticationServices.JwtTokenGenerator.Options;

public class JwtAuthenticationOptions
{
    public const string SectionName = "Authentication:Jwt";

    public string Issuer { get; set; } = string.Empty;

    public string Audience { get; set; } = string.Empty;

    public string SecretKey { get; set; } = string.Empty;

    public int ExpirationMinutes { get; set; } = 30;
}