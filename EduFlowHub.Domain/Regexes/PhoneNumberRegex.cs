using System.Text.RegularExpressions;

namespace EduFlowHub.Domain.Regexes;

public static class PhoneNumberRegex
{
    private static readonly Regex Regex = new(
        @"^\+[1-9]\d{7,14}$",
        RegexOptions.Compiled | RegexOptions.CultureInvariant);
    
    public static bool IsMatch(string phoneNumber)
    {
        return Regex.IsMatch(phoneNumber.Trim());
    }
}