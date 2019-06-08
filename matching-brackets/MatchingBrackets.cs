using System;
using System.Linq;
using System.Text.RegularExpressions;

public static class MatchingBrackets
{
    public static bool IsPaired(string input)
    {
        var brackets = new string(input.Where(c => "[]{}()".Contains(c)).ToArray());
        var previousLength = brackets.Length;

        while (brackets.Length > 0)
        {
            brackets = brackets
                .Replace("[]", string.Empty)
                .Replace("{}", string.Empty)
                .Replace("()", string.Empty);

            if (brackets.Length == previousLength)
                return false;

            previousLength = brackets.Length;
        }

        return true;
    }
}
