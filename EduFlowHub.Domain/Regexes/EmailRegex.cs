using System.Text.RegularExpressions;

namespace EduFlowHub.Domain.Regexes;

public static class EmailRegex
{
    private static readonly Regex Regex = new(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.Compiled | RegexOptions.CultureInvariant);

    public static bool IsMatch(string email)
    {
        return Regex.IsMatch(email.Trim());
    }
}