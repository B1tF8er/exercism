using System;
using System.Collections.Generic;
using System.Linq;

public static class ParallelLetterFrequency
{
    public static Dictionary<char, int> Calculate(IEnumerable<string> texts)
    {
        var letterGroups = texts
            .AsParallel()
            .SelectMany(text => text
                .ToLower()
                .Where(char.IsLetter)
                .GroupBy(letter => letter)
            );

        var result = new Dictionary<char, int>();

        foreach (var group in letterGroups)
        {
            if (result.ContainsKey(group.Key))
                result[group.Key] += group.Count();
            else
                result[group.Key] = group.Count();
        }

        return result;
    }
}