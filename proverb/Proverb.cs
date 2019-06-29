using System;
using System.Linq;

public static class Proverb
{
    public static string[] Recite(string[] subjects) =>
        Enumerable.Range(1, subjects.Length)
            .Select((lineIndex) =>
                (lineIndex == subjects.Length) 
                ? $"And all for the want of a {subjects[0]}."
                : $"For want of a {subjects[lineIndex - 1]} the {subjects[lineIndex]} was lost.")
            .ToArray();
}