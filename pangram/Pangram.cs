using System;
using System.Linq;

public static class Pangram
{
    public static bool IsPangram(string input) =>
        input.ToLowerInvariant()
            .Where(c => c >= 'a' && c <= 'z')
            .Distinct()
            .Count() == 26;
}
