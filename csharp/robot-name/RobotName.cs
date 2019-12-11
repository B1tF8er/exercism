using System;
using System.Collections.Generic;
using System.Linq;

public class Robot
{
    private static readonly HashSet<string> names = new HashSet<string>();

    private static readonly char[] alphabet = Enumerable
        .Range('A', 'Z' - 'A' + 1)
        .Select(letter => (Char)letter)
        .ToArray();

    private static Random random = new Random();

    private char RandomLetter => alphabet[random.Next(26)];

    private string RandomNumber => random.Next(1000).ToString("000");

    public string Name { get; private set; }

    public Robot() => Name = GenerateName();

    public void Reset() => Name = GenerateName();

    private string GenerateName()
    {
        var name = $"{RandomLetter}{RandomLetter}{RandomNumber}";

        if (names.Add(name))
            return name;
        else
            return GenerateName();
    }
}