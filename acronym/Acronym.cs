using System;
using System.Linq;

public static class Acronym
{
    private static readonly char[] separators = new [] { ' ', '-', '_' };

    public static string Abbreviate(string phrase) =>
        string.Join(
            string.Empty,
            phrase
                .Split(separators, StringSplitOptions.RemoveEmptyEntries)
                .Where(word => word.Any(char.IsLetter))
                .Select(word => char.ToUpper(word[0]))
        );
}