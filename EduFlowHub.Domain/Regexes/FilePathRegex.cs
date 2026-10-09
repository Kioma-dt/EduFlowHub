using System.Text.RegularExpressions;

namespace EduFlowHub.Domain.Regexes;

public static class FilePathRegex
{
    private static readonly Regex Regex = new(
        @"^(?!/)(?!.*(?:^|/)\.\.(?:/|$))(?!.*\\)[a-zA-Z0-9_/-]+(?:\.[a-zA-Z0-9]+)?$",
        RegexOptions.Compiled | RegexOptions.CultureInvariant);

    public static bool IsMatch(string email)
    {
        return Regex.IsMatch(email.Trim());
    }
}