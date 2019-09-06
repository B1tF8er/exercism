using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public static class WordCount
{
    public static IDictionary<string, int> CountWords(string phrase) =>
        Regex.Split(phrase, @"(?:\\n)|(\w[-\w']*\w|\w)")
            .Where(word => !string.IsNullOrEmpty(word))
            .Where(word => !word.All(c => !char.IsLetterOrDigit(c)))
            .GroupBy(word => word.ToLower())
            .ToDictionary(group => group.Key, group => group.Count());
}