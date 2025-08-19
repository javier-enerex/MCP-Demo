using System.Net.Mail;

public static class EmailValidator
{
    /// <summary>Validate a single email like "user@domain.com".</summary>
    public static bool IsValid(string? email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;
        email = email.Trim().Trim('"', '\'');
        try
        {
            // MailAddress both validates and normalizes
            _ = new MailAddress(email).Address;
            return true;
        }
        catch { return false; }
    }

    /// <summary>
    /// Validate a single token like "user@domain.com|2".
    /// Ensures the email is valid and the tag is a positive integer.
    /// </summary>
    public static bool IsValidPipe(string? emailWithTag, char sep = '|')
    {
        if (string.IsNullOrWhiteSpace(emailWithTag)) return false;

        var token = emailWithTag.Trim().Trim('"', '\'');
        var parts = token.Split(sep, 2, StringSplitOptions.TrimEntries);

        if (!IsValid(parts[0])) return false;
        if (parts.Length == 1)  return true; // treat missing tag as still valid email

        return int.TryParse(parts[1], out var tag) && tag > 0;
    }

    /// <summary>
    /// Validate a list like "a@b.com; c@d.com" or "a@b.com c@d.com" or "a@b.com,c@d.com".
    /// If a token has a pipe (e.g., "a@b.com|1"), the part after '|' is ignored for validation.
    /// Returns true only if *every* token is a valid email.
    /// </summary>
    public static bool AreAllValid(string? input)
    {
        return TryGetInvalids(input, out var invalids) && invalids.Count == 0;
    }

    /// <summary>
    /// Same as AreAllValid, but also returns the invalid tokens (original form, trimmed).
    /// </summary>
    public static bool TryGetInvalids(string? input, out List<string> invalidTokens)
    {
        invalidTokens = new List<string>();
        if (string.IsNullOrWhiteSpace(input)) return true; // empty -> no invalids

        var tokens = input.Split(new[] { ';', ',', ' ', '\t', '\r', '\n' },
                                 StringSplitOptions.RemoveEmptyEntries);

        foreach (var raw in tokens)
        {
            var t = raw.Trim().Trim('"', '\'');
            var emailPart = t.Contains('|') ? t.Split('|', 2)[0].Trim() : t;

            if (!IsValid(emailPart))
                invalidTokens.Add(t);
        }

        return invalidTokens.Count == 0;
    }
}
