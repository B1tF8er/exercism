using System;
using System.Collections.Generic;
using System.Linq;

public static class ParallelLetterFrequency
{
    public static Dictionary<char, int> Calculate(IEnumerable<string> texts) =>
        texts.AsParallel()
            .SelectMany(text => text.ToLower())
            .Where(char.IsLetter)
            .GroupBy(letter => letter)
            .ToDictionary(group => group.Key, group => group.Count());
}